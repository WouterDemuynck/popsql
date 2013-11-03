using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql.Text
{
    /// <summary>
    /// Represents a writer that provides a means of generating streams of SQL statement text.
    /// </summary>
    public class SqlWriter : IDisposable
    {
        private TextWriter _writer;
        private readonly bool _canDisposeWriter;
        private bool _isDisposed;

        private readonly SqlWriterStateManager _stateManager;
        private bool _hasPendingSpace;
        
        /// <summary>
        /// Represents the SQL NULL value.
        /// </summary>
        public const string SqlNull = "NULL";

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlWriter"/> class that writes to the
        /// specified <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="StringBuilder"/> to write to.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="builder"/> argument is <see langword="null"/>.
        /// </exception>
        public SqlWriter(StringBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException("builder");

            _writer = new StringWriter(builder, CultureInfo.InvariantCulture);
            _canDisposeWriter = true;
            _stateManager = new SqlWriterStateManager();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlWriter"/> class that writes to the
        /// specified <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="TextWriter"/> to write to.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="writer"/> argument is <see langword="null"/>.
        /// </exception>
        public SqlWriter(TextWriter writer)
        {
            if (writer == null) throw new ArgumentNullException("writer");

            _writer = writer;
            _canDisposeWriter = false;
            _stateManager = new SqlWriterStateManager();
        }

        /// <summary>
        /// Finalizes the current instance of the <see cref="SqlWriter"/> class.
        /// </summary>
        ~SqlWriter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the current state of the <see cref="SqlWriter"/>.
        /// </summary>
        public SqlWriterState WriteState
        {
            get
            {
                return _stateManager.CurrentState;
            }
        }

        /// <summary>
        /// Writes a SQL UNION operator to the output stream.
        /// </summary>
        public void WriteUnion()
        {
            EnsureNotDisposed();
            Write("UNION");
            _stateManager.RequestState(SqlWriterState.Union);
        }

        /// <summary>
        /// Writes the start of a SQL SELECT statement to the output stream.
        /// </summary>
        public void WriteStartSelect()
        {
            EnsureNotDisposed();
            Write("SELECT");
            _stateManager.RequestState(SqlWriterState.StartSelect);
        }

        /// <summary>
        /// Writes the end of a SQL SELECT statement to the output stream.
        /// </summary>
        public void WriteEndSelect()
        {
            EnsureNotDisposed();
            _stateManager.RequestState(SqlWriterState.EndSelect);
        }

        /// <summary>
        /// Writes the specified column to the ouput stream.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to write.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="columnName"/> argument is <see langword="null"/>,
        /// an empty string, or a string containing only white-space characters.
        /// </exception>
        public void WriteColumn(string columnName)
        {
            WriteColumn(null, columnName, null);
        }

        /// <summary>
        /// Writes the specified column to the output stream.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column to write.
        /// </param>
        /// <param name="alias">
        /// The alias of the column to write.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="columnName"/> argument is <see langword="null"/>,
        /// an empty string, or a string containing only white-space characters.
        /// </exception>
        public void WriteColumn(string columnName, string alias)
        {
            WriteColumn(null, columnName, alias);
        }

        /// <summary>
        /// Writes the specified column to the output stream.
        /// </summary>
        /// <param name="tableName">
        /// The name of the table of the column to write.
        /// </param>
        /// <param name="columnName">
        /// The name of the column to write.
        /// </param>
        /// <param name="alias">
        /// The alias of the column to write.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="columnName"/> argument is <see langword="null"/>,
        /// an empty string, or a string containing only white-space characters.
        /// </exception>
        public void WriteColumn(string tableName, string columnName, string alias)
        {
            EnsureNotDisposed();
            if (string.IsNullOrWhiteSpace(columnName)) throw new ArgumentNullException("columnName");

            switch (WriteState)
            {
                case SqlWriterState.Select:
                case SqlWriterState.Into:
                case SqlWriterState.OrderBy:
                case SqlWriterState.Set:
                    WriteRaw(",");
                    break;

                case SqlWriterState.StartInto: 
                    WriteOpenParenthesis();
                    break;

                case SqlWriterState.StartWhere:
                case SqlWriterState.StartOn:
                case SqlWriterState.StartExpression:
                    WriteOpenParenthesis();
                    break;
            }

            if (!string.IsNullOrWhiteSpace(tableName))
            {
                Write(FormatTableName(tableName));
                WriteRaw(".");

                // Prevent a space after the dot operator.
                _hasPendingSpace = false;
            }

            Write(FormatColumnName(columnName));

            if (!string.IsNullOrWhiteSpace(alias))
            {
                Write("AS");
                Write(FormatColumnName(alias));
            }

            switch (WriteState)
            {
                case SqlWriterState.StartSelect:
                    _stateManager.RequestState(SqlWriterState.Select);
                    break;

                case SqlWriterState.StartInto:
                    _stateManager.RequestState(SqlWriterState.Into);
                    break;

                case SqlWriterState.StartSet:
                    _stateManager.RequestState(SqlWriterState.Set);
                    break;

                case SqlWriterState.StartWhere:
                case SqlWriterState.StartOn:
                    _stateManager.RequestState(SqlWriterState.StartExpression);
                    break;

                case SqlWriterState.Expression:
                    WriteCloseParenthesis();
                    _stateManager.RequestState(new Dictionary<SqlWriterState, SqlWriterState>
                    {
                        { SqlWriterState.StartOn,    SqlWriterState.On },
                        { SqlWriterState.StartWhere, SqlWriterState.Where },
                    });
                    break;

                case SqlWriterState.StartOrderBy:
                    _stateManager.RequestState(SqlWriterState.OrderBy);
                    break;
            }
        }

        /// <summary>
        /// Writes an opening parenthesis to the output stream.
        /// </summary>
        public void WriteOpenParenthesis()
        {
            Write("(");
            _hasPendingSpace = false;
        }

        /// <summary>
        /// Writes an closing parenthesis to the output stream.
        /// </summary>
        public void WriteCloseParenthesis()
        {
            WriteRaw(")");
        }

        /// <summary>
        /// Writes the start of a SQL FROM clause to the output stream.
        /// </summary>
        public void WriteStartFrom()
        {
            EnsureNotDisposed();
            Write("FROM");
            _stateManager.RequestState(SqlWriterState.StartFrom);
        }

        /// <summary>
        /// Writes the start of a SQL JOIN clause to the output stream.
        /// </summary>
        public void WriteStartJoin(SqlJoinType type = SqlJoinType.Default)
        {
            EnsureNotDisposed();
            switch (type)
            {
                case SqlJoinType.Inner:
                    Write("INNER");
                    break;

                case SqlJoinType.Left:
                    Write("LEFT");
                    break;

                case SqlJoinType.Right:
                    Write("RIGHT");
                    break;
            }
            Write("JOIN");
            _stateManager.RequestState(SqlWriterState.StartJoin);
        }

        /// <summary>
        /// Writes the start of a SQL JOIN condition to the output stream.
        /// </summary>
        public void WriteStartOn()
        {
            EnsureNotDisposed();
            Write("ON");
            _stateManager.RequestState(SqlWriterState.StartOn);
        }

        /// <summary>
        /// Writes the start of a SQL WHERE clause to the output stream.
        /// </summary>
        public void WriteStartWhere()
        {
            EnsureNotDisposed();
            Write("WHERE");
            _stateManager.RequestState(SqlWriterState.StartWhere);
        }

        /// <summary>
        /// Writes the specified table to the output stream.
        /// </summary>
        /// <param name="tableName">
        /// The name of the table to write.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="tableName"/> argument is <see langword="null"/>,
        /// an empty string, or a string containing only white-space characters.
        /// </exception>
        public void WriteTable(string tableName)
        {
            WriteTable(tableName, null);
        }

        /// <summary>
        /// Writes the specified table to the output stream.
        /// </summary>
        /// <param name="tableName">
        /// The name of the table to write.
        /// </param>
        /// <param name="alias">
        /// The alias of the table to write.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="tableName"/> argument is <see langword="null"/>,
        /// an empty string, or a string containing only white-space characters.
        /// </exception>
        public void WriteTable(string tableName, string alias)
        {
            EnsureNotDisposed();
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentNullException("tableName");

            if (WriteState == SqlWriterState.From)
            {
                WriteRaw(",");
            }

            Write(FormatTableName(tableName));

            if (!string.IsNullOrWhiteSpace(alias))
            {
                Write(FormatTableName(alias));
            }

            switch (WriteState)
            {
                case SqlWriterState.StartFrom:
                    _stateManager.RequestState(SqlWriterState.From);
                    break;

                case SqlWriterState.StartUpdate:
                    _stateManager.RequestState(SqlWriterState.Update);
                    break;

                case SqlWriterState.StartJoin:
                    _stateManager.RequestState(SqlWriterState.Join);
                    break;
            }
        }

        /// <summary>
        /// Writes the start of a SQL ORDER BY clause to the output stream.
        /// </summary>
        public void WriteStartOrderBy()
        {
            EnsureNotDisposed();
            Write("ORDER BY");
            _stateManager.RequestState(SqlWriterState.StartOrderBy);
        }

        /// <summary>
        /// Writes the specified sort order to the output stream.
        /// </summary>
        /// <param name="sortOrder">
        /// The sort order to write to the output stream.
        /// </param>
        public void WriteSortOrder(SqlSortOrder sortOrder)
        {
            EnsureNotDisposed();
            switch (sortOrder)
            {
                case SqlSortOrder.Ascending:
                    Write("ASC");
                    break;

                case SqlSortOrder.Descending:
                    Write("DESC");
                    break;

                default:
                    throw new InvalidEnumArgumentException("operator", (int)sortOrder, typeof(SqlBinaryOperator));
            }
        }

        /// <summary>
        /// Writes the specified operator to the output stream.
        /// </summary>
        /// <param name="operator">
        /// The operator to write to the output stream.
        /// </param>
        public void WriteOperator(SqlBinaryOperator @operator)
        {
            EnsureNotDisposed();
            SqlWriterState state = SqlWriterState.Expression;
            switch (@operator)
            {
                case SqlBinaryOperator.And:
                    state = SqlWriterState.StartExpression;
                    Write("AND");
                    break;

                case SqlBinaryOperator.Equal:
                    Write("=");
                    break;

                case SqlBinaryOperator.GreaterThan:
                    Write(">");
                    break;

                case SqlBinaryOperator.GreaterThanOrEqual:
                    Write(">=");
                    break;

                case SqlBinaryOperator.LessThan:
                    Write("<");
                    break;

                case SqlBinaryOperator.LessThanOrEqual:
                    Write("<=");
                    break;

                case SqlBinaryOperator.Like:
                    Write("LIKE");
                    break;

                case SqlBinaryOperator.NotEqual:
                    Write("<>");
                    break;

                case SqlBinaryOperator.Or:
                    state = SqlWriterState.StartExpression;
                    Write("OR");
                    break;

                default:
                    throw new InvalidEnumArgumentException("operator", (int)@operator, typeof(SqlBinaryOperator));
            }

            _stateManager.RequestState(state);
        }

        /// <summary>
        /// Writes the start of a SQL UPDATE statement to the output stream.
        /// </summary>
        public void WriteStartUpdate()
        {
            EnsureNotDisposed();
            Write("UPDATE");
            _stateManager.RequestState(SqlWriterState.StartUpdate);
        }

        /// <summary>
        /// Writes the start of a SQL SET clause to the output stream.
        /// </summary>
        public void WriteStartSet()
        {
            EnsureNotDisposed();
            Write("SET");
            _stateManager.RequestState(SqlWriterState.StartSet);
        }

        /// <summary>
        /// Writes the start of a SQL INSERT statement to the output stream.
        /// </summary>
        public void WriteStartInsert()
        {
            EnsureNotDisposed();
            Write("INSERT");
            _stateManager.RequestState(SqlWriterState.StartInsert);
        }

        /// <summary>
        /// Writes the start of a SQL INTO statement to the output stream.
        /// </summary>
        public void WriteStartInto()
        {
            EnsureNotDisposed();
            Write("INTO");
            _stateManager.RequestState(SqlWriterState.StartInto);
        }

        /// <summary>
        /// Writes the start of a SQL VALUES clause to the output stream.
        /// </summary>
        public void WriteStartValues()
        {
            EnsureNotDisposed();
            switch (WriteState)
            {
                case SqlWriterState.Into:
                    WriteCloseParenthesis();
                    Write("VALUES");
                    break;

                case SqlWriterState.EndValues:
                    WriteRaw(",");
                    break;
            }
            _stateManager.RequestState(SqlWriterState.StartValues);
        }

        /// <summary>
        /// Writes the end of a SQL VALUES clause to the output stream.
        /// </summary>
        public void WriteEndValues()
        {
            EnsureNotDisposed();
            WriteCloseParenthesis();
            _stateManager.RequestState(SqlWriterState.EndValues);
        }

        /// <summary>
        /// Writes the start of a SQL DELETE statement to the output stream.
        /// </summary>
        public void WriteStartDelete()
        {
            EnsureNotDisposed();
            Write("DELETE");
            _stateManager.RequestState(SqlWriterState.StartDelete);
        }

        /// <summary>
        /// Writes the specified SQL value to the output stream.
        /// </summary>
        /// <param name="value">
        /// The value to write to the output stream.
        /// </param>
        public void WriteValue(object value)
        {
            EnsureNotDisposed();

            switch (WriteState)
            {
                case SqlWriterState.StartValues:
                    Write("(");
                    _hasPendingSpace = false;
                    break;

                case SqlWriterState.Values:
                    WriteRaw(",");
                    break;

                case SqlWriterState.Set:
                    Write("=");
                    break;
            }

            if (value == null)
            {
                Write(SqlNull);
                return;
            }

            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.String:
                case TypeCode.Char:
                    Write(FormatString(Convert.ToString(value, CultureInfo.InvariantCulture)));
                    break;

                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    Write(Convert.ToString(value, CultureInfo.InvariantCulture));
                    break;

                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    Write(Convert.ToString(value, CultureInfo.InvariantCulture));
                    break;

                default:
                    Write(FormatString(Convert.ToString(value, CultureInfo.InvariantCulture)));
                    break;
            }

            switch (WriteState)
            {
                case SqlWriterState.StartValues:
                    _stateManager.RequestState(SqlWriterState.Values);
                    break;

                case SqlWriterState.Expression:
                    WriteCloseParenthesis();
                    _stateManager.RequestState(new Dictionary<SqlWriterState, SqlWriterState>
                    {
                        { SqlWriterState.StartOn,    SqlWriterState.On },
                        { SqlWriterState.StartWhere, SqlWriterState.Where },
                    });
                    break;
            }
        }

        /// <summary>
        /// Writes the specified SQL parameter to the output stream.
        /// </summary>
        /// <param name="parameterName">
        /// The value to write to the output stream.
        /// </param>
        public void WriteParameter(string parameterName)
        {
            EnsureNotDisposed();

            switch (WriteState)
            {
                case SqlWriterState.StartValues:
                    Write("(");
                    _hasPendingSpace = false;
                    break;

                case SqlWriterState.Values:
                    WriteRaw(",");
                    break;

                case SqlWriterState.Set:
                    Write("=");
                    break;
            }

            Write(FormatParameterName(parameterName));

            switch (WriteState)
            {
                case SqlWriterState.StartValues:
                    _stateManager.RequestState(SqlWriterState.Values);
                    break;

                case SqlWriterState.Expression:
                    WriteCloseParenthesis();
                    _stateManager.RequestState(new Dictionary<SqlWriterState, SqlWriterState>
                    {
                        { SqlWriterState.StartOn,    SqlWriterState.On },
                        { SqlWriterState.StartWhere, SqlWriterState.Where },
                    });
                    break;
            }
        }

        /// <summary>
        /// Writes the specified string to the output stream, including any pending formatting.
        /// </summary>
        /// <param name="value">
        /// The <see cref="String"/> to write to the output stream.
        /// </param>
        protected void Write(string value)
        {
            if (_hasPendingSpace)
            {
                WriteRaw(" ");
                _hasPendingSpace = false;
            }

            WriteRaw(value);
            _hasPendingSpace = true;
        }

        /// <summary>
        /// Writes the specified string to the output stream, ignoring any pending formatting.
        /// </summary>
        /// <param name="value">
        /// The <see cref="String"/> to write to the output stream.
        /// </param>
        protected void WriteRaw(string value)
        {
            _writer.Write(value);
        }

        /// <summary>
        /// Formats the specified string for the current SQL dialect. The default implementation 
        /// returns the table name enclosed in single quotes.
        /// </summary>
        /// <param name="value">
        /// The SQL string to format.
        /// </param>
        /// <returns>
        /// The formatted SQL string.
        /// </returns>
        protected virtual string FormatString(string value)
        {
            return "'" + value + "'";
        }

        /// <summary>
        /// Formats the specified table name for the current SQL dialect. The default implementation 
        /// returns the table name enclosed in square brackets.
        /// </summary>
        /// <param name="tableName">
        /// The table name to format.
        /// </param>
        /// <returns>
        /// The formatted table name.
        /// </returns>
        protected virtual string FormatTableName(string tableName)
        {
            return "[" + tableName + "]";
        }

        /// <summary>
        /// Formats the specified parameter name for the current SQL dialect. The default implementation 
        /// returns the parameter name prefixed with an 'at' sign ('@').
        /// </summary>
        /// <param name="parameterName">
        /// The parameter name to format.
        /// </param>
        /// <returns>
        /// The formatted parameter name.
        /// </returns>
        protected virtual string FormatParameterName(string parameterName)
        {
            return "@" + parameterName;
        }

        /// <summary>
        /// Formats the specified column name for the current SQL dialect. The default implementation
        /// returns the column name enclosed in square brackets.
        /// </summary>
        /// <param name="columnName">
        /// The column name to format.
        /// </param>
        /// <returns>
        /// The formatted column name.
        /// </returns>
        protected virtual string FormatColumnName(string columnName)
        {
            return "[" + columnName + "]";
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all unmanaged resources used by the current instance of the <see cref="SqlWriter"/> class and, 
        /// optionally, releases all managed resources as well.
        /// </summary>
        /// <param name="isDisposing">
        /// <see langword="true"/> to release managed resources as well as unmanaged resources; otherwise, <see langword="false"/>.
        /// </param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed) return;

            if (isDisposing)
            {
                _stateManager.Close();
            }

            if (_canDisposeWriter && _writer != null)
            {
                _writer.Dispose();
                _writer = null;
            }

            _isDisposed = true;
        }

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> when the current instance of the
        /// <see cref="SqlWriter"/> class has already been disposed.
        /// </summary>
        protected void EnsureNotDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
