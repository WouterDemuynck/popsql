using System;
using System.Collections.Generic;
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
        /// Initializes a new instance of the <see cref="SqlWriter"/> class that writes to the
        /// specified <paramref name="StringBuilder"/>.
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
        /// Writes the start of a SQL SELECT statement to the output stream.
        /// </summary>
        public void WriteStartSelect()
        {
            EnsureNotDisposed();
            Write("SELECT");
            _stateManager.RequestState(SqlWriterState.StartSelect);
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

            if (WriteState == SqlWriterState.Select)
            {
                WriteRaw(",");
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
            }
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
            }
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
            _stateManager.RequestState(SqlWriterState.Into);
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
