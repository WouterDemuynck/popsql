using Popsql.Text;
using System;
using System.Data;
using System.Linq;
using System.Text;

namespace Popsql
{
    /// <summary>
    /// Provides extension methods for writing SQL expression trees to SQL text.
    /// </summary>
    public static class SqlStatementExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="SqlSelect"/> expression tree to an <see cref="IDbCommand"/>
        /// instance for the specified <paramref name="connection" />.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlSelect"/> to convert to an <see cref="IDbCommand"/>.
        /// </param>
        /// <param name="connection">
        /// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
        /// the specified SQL expression tree.
        /// </returns>
        /// <remarks>
        /// Any <see cref="SqlParameter"/> members of the <see cref="SqlSelect"/> expression are
        /// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
        /// the returned <see cref="IDbCommand"/>.
        /// </remarks>
        public static IDbCommand ToCommand(this SqlSelect sql, IDbConnection connection)
        {
            return connection.CreateCommandInternal(sql);
        }

        /// <summary>
        /// Converts the specified <see cref="SqlUnion"/> expression tree to an <see cref="IDbCommand"/>
        /// instance for the specified <paramref name="connection" />.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlSelect"/> to convert to an <see cref="IDbCommand"/>.
        /// </param>
        /// <param name="connection">
        /// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
        /// the specified SQL expression tree.
        /// </returns>
        /// <remarks>
        /// Any <see cref="SqlParameter"/> members of the <see cref="SqlSelect"/> expression are
        /// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
        /// the returned <see cref="IDbCommand"/>.
        /// </remarks>
        public static IDbCommand ToCommand(this SqlUnion sql, IDbConnection connection)
        {
            return connection.CreateCommandInternal(sql);
        }

        /// <summary>
        /// Converts the specified <see cref="SqlInsert"/> expression tree to an <see cref="IDbCommand"/>
        /// instance for the specified <paramref name="connection" />.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlInsert"/> to convert to an <see cref="IDbCommand"/>.
        /// </param>
        /// <param name="connection">
        /// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
        /// the specified SQL expression tree.
        /// </returns>
        /// <remarks>
        /// Any <see cref="SqlParameter"/> members of the <see cref="SqlInsert"/> expression are
        /// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
        /// the returned <see cref="IDbCommand"/>.
        /// </remarks>
        public static IDbCommand ToCommand(this SqlInsert sql, IDbConnection connection)
        {
            return connection.CreateCommandInternal(sql);
        }

        /// <summary>
        /// Converts the specified <see cref="SqlUpdate"/> expression tree to an <see cref="IDbCommand"/>
        /// instance for the specified <paramref name="connection" />.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlUpdate"/> to convert to an <see cref="IDbCommand"/>.
        /// </param>
        /// <param name="connection">
        /// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
        /// the specified SQL expression tree.
        /// </returns>
        /// <remarks>
        /// Any <see cref="SqlParameter"/> members of the <see cref="SqlUpdate"/> expression are
        /// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
        /// the returned <see cref="IDbCommand"/>.
        /// </remarks>
        public static IDbCommand ToCommand(this SqlUpdate sql, IDbConnection connection)
        {
            return connection.CreateCommandInternal(sql);
        }

        /// <summary>
        /// Converts the specified <see cref="SqlDelete"/> expression tree to an <see cref="IDbCommand"/>
        /// instance for the specified <paramref name="connection" />.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlDelete"/> to convert to an <see cref="IDbCommand"/>.
        /// </param>
        /// <param name="connection">
        /// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
        /// the specified SQL expression tree.
        /// </returns>
        /// <remarks>
        /// Any <see cref="SqlParameter"/> members of the <see cref="SqlDelete"/> expression are
        /// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
        /// the returned <see cref="IDbCommand"/>.
        /// </remarks>
        public static IDbCommand ToCommand(this SqlDelete sql, IDbConnection connection)
        {
            return connection.CreateCommandInternal(sql);
        }

        /// <summary>
        /// Converts the specified <see cref="SqlDelete"/> expression tree to an <see cref="IDbCommand"/>
        /// instance for the specified <paramref name="connection" />.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlDelete"/> to convert to an <see cref="IDbCommand"/>.
        /// </param>
        /// <param name="connection">
        /// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
        /// the specified SQL expression tree.
        /// </returns>
        /// <remarks>
        /// Any <see cref="SqlParameter"/> members of the <see cref="SqlDelete"/> expression are
        /// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
        /// the returned <see cref="IDbCommand"/>.
        /// </remarks>
        public static IDbCommand CreateCommand(this IDbConnection connection, SqlDelete sql)
        {
            return connection.CreateCommandInternal(sql);
        }

        /// <summary>
        /// Converts the specified <see cref="SqlUpdate"/> expression tree to an <see cref="IDbCommand"/>
        /// instance for the specified <paramref name="connection" />.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlUpdate"/> to convert to an <see cref="IDbCommand"/>.
        /// </param>
        /// <param name="connection">
        /// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
        /// the specified SQL expression tree.
        /// </returns>
        /// <remarks>
        /// Any <see cref="SqlParameter"/> members of the <see cref="SqlUpdate"/> expression are
        /// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
        /// the returned <see cref="IDbCommand"/>.
        /// </remarks>
        public static IDbCommand CreateCommand(this IDbConnection connection, SqlUpdate sql)
        {
            return connection.CreateCommandInternal(sql);
        }

        /// <summary>
        /// Converts the specified <see cref="SqlInsert"/> expression tree to an <see cref="IDbCommand"/>
        /// instance for the specified <paramref name="connection" />.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlInsert"/> to convert to an <see cref="IDbCommand"/>.
        /// </param>
        /// <param name="connection">
        /// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
        /// the specified SQL expression tree.
        /// </returns>
        /// <remarks>
        /// Any <see cref="SqlParameter"/> members of the <see cref="SqlInsert"/> expression are
        /// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
        /// the returned <see cref="IDbCommand"/>.
        /// </remarks>
        public static IDbCommand CreateCommand(this IDbConnection connection, SqlInsert sql)
        {
            return connection.CreateCommandInternal(sql);
        }

        /// <summary>
        /// Converts the specified <see cref="SqlUnion"/> expression tree to an <see cref="IDbCommand"/>
        /// instance for the specified <paramref name="connection" />.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlUnion"/> to convert to an <see cref="IDbCommand"/>.
        /// </param>
        /// <param name="connection">
        /// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
        /// the specified SQL expression tree.
        /// </returns>
        /// <remarks>
        /// Any <see cref="SqlParameter"/> members of the <see cref="SqlUnion"/> expression are
        /// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
        /// the returned <see cref="IDbCommand"/>.
        /// </remarks>
        public static IDbCommand CreateCommand(this IDbConnection connection, SqlUnion sql)
        {
            return connection.CreateCommandInternal(sql);
        }

        /// <summary>
        /// Converts the specified <see cref="SqlSelect"/> expression tree to an <see cref="IDbCommand"/>
        /// instance for the specified <paramref name="connection" />.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlSelect"/> to convert to an <see cref="IDbCommand"/>.
        /// </param>
        /// <param name="connection">
        /// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
        /// </param>
        /// <returns>
        /// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
        /// the specified SQL expression tree.
        /// </returns>
        /// <remarks>
        /// Any <see cref="SqlParameter"/> members of the <see cref="SqlSelect"/> expression are
        /// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
        /// the returned <see cref="IDbCommand"/>.
        /// </remarks>
        public static IDbCommand CreateCommand(this IDbConnection connection, SqlSelect sql)
        {
            return connection.CreateCommandInternal(sql);
        }

        /// <summary>
        /// Converts the specified <see cref="SqlSelect"/> expression tree to SQL text.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlSelect"/> to convert to SQL text.
        /// </param>
        /// <returns>
        /// The SQL text for the specified SQL expression tree.
        /// </returns>
        public static string ToSql(this SqlSelect sql)
        {
            return ToSqlInternal(sql);
        }

        /// <summary>
        /// Converts the specified <see cref="SqlUnion"/> expression tree to SQL text.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlUnion"/> to convert to SQL text.
        /// </param>
        /// <returns>
        /// The SQL text for the specified SQL expression tree.
        /// </returns>
        public static string ToSql(this SqlUnion sql)
        {
            return sql.ToSqlInternal();
        }

        /// <summary>
        /// Converts the specified <see cref="SqlDelete"/> expression tree to SQL text.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlDelete"/> to convert to SQL text.
        /// </param>
        /// <returns>
        /// The SQL text for the specified SQL expression tree.
        /// </returns>
        public static string ToSql(this SqlDelete sql)
        {
            return sql.ToSqlInternal();
        }

        /// <summary>
        /// Converts the specified <see cref="SqlUpdate"/> expression tree to SQL text.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlUpdate"/> to convert to SQL text.
        /// </param>
        /// <returns>
        /// The SQL text for the specified SQL expression tree.
        /// </returns>
        public static string ToSql(this SqlUpdate sql)
        {
            return sql.ToSqlInternal();
        }

        /// <summary>
        /// Converts the specified <see cref="SqlInsert"/> expression tree to SQL text.
        /// </summary>
        /// <param name="sql">
        /// The <see cref="SqlInsert"/> to convert to SQL text.
        /// </param>
        /// <returns>
        /// The SQL text for the specified SQL expression tree.
        /// </returns>
        public static string ToSql(this SqlInsert sql)
        {
            return sql.ToSqlInternal();
        }

        private static string ToSqlInternal(this SqlStatement sql, Action<SqlParameter> parameterVisited = null)
        {
            if (sql == null) throw new ArgumentNullException("sql");

            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                SqlWriterVisitor visitor = new SqlWriterVisitor(writer);
                visitor.ParameterVisited = parameterVisited;
                visitor.Visit(sql);
            }

            return builder.ToString();
        }

        private static IDbCommand CreateCommandInternal(this IDbConnection connection, SqlStatement sql)
        {
            if (sql == null) throw new ArgumentNullException("sql");
            if (connection == null) throw new ArgumentNullException("connection");

            var command = connection.CreateCommand();
            command.CommandText = sql.ToSqlInternal(
                    p =>
                    {
                        IDbDataParameter parameter = command.CreateParameter();
                        parameter.ParameterName = p.ParameterName;
                        parameter.Value = p.Value;
                        command.Parameters.Add(parameter);
                    });

            return command;
        }

        private class SqlWriterVisitor : SqlExpressionVisitor
        {
            private readonly SqlWriter _writer;

            public SqlWriterVisitor(SqlWriter writer)
            {
                if (writer == null) throw new ArgumentNullException("writer");

                _writer = writer;
            }

            protected override SqlExpression VisitSelect(SqlSelect expression)
            {
                _writer.WriteStartSelect();
                if (expression.Columns.Any())
                {
                    foreach (var column in expression.Columns)
                    {
                        _writer.WriteColumn(column.TableName, column.ColumnName, column.Alias);
                    }
                }
                else
                {
                    // TODO: Don't know what will happen here.
                    _writer.WriteColumn("*");
                }

                _writer.WriteStartFrom();
                _writer.WriteTable(expression.Table.TableName, expression.Table.Alias);

                foreach (var join in expression.Joins)
                {
                    Visit(join);
                }

                if (expression.Predicate != null)
                {
                    _writer.WriteStartWhere();
                    Visit(expression.Predicate);
                }

                if (expression.Sorting.Any())
                {
                    _writer.WriteStartOrderBy();
                    Visit(expression.Sorting);
                }
                _writer.WriteEndSelect();
                return expression;
            }

            protected override SqlExpression VisitUnion(SqlUnion expression)
            {
                bool writeUnion = false;
                foreach (SqlSelect statement in expression.Statements)
                {
                    if (writeUnion)
                    {
                        _writer.WriteUnion();
                    }
                    _writer.WriteOpenParenthesis();
                    Visit(statement);
                    _writer.WriteCloseParenthesis();
                    writeUnion = true;
                }
                return expression;
            }

            protected override SqlExpression VisitDelete(SqlDelete expression)
            {
                _writer.WriteStartDelete();
                _writer.WriteStartFrom();
                _writer.WriteTable(expression.Table.TableName, expression.Table.Alias);

                if (expression.Predicate != null)
                {
                    _writer.WriteStartWhere();
                    Visit(expression.Predicate);
                }

                return expression;
            }

            protected override SqlExpression VisitUpdate(SqlUpdate expression)
            {
                _writer.WriteStartUpdate();
                Visit(expression.Table);

                _writer.WriteStartSet();
                foreach (var value in expression.Values)
                {
                    Visit(value.Column);
                    Visit(value.Value);
                }

                if (expression.Predicate != null)
                {
                    _writer.WriteStartWhere();
                    Visit(expression.Predicate);
                }
                return expression;
            }

            protected override SqlExpression VisitInsert(SqlInsert expression)
            {
                _writer.WriteStartInsert();
                _writer.WriteStartInto();
                Visit(expression.Table);

                if (expression.Columns.Any())
                {
                    Visit(expression.Columns);
                }

                bool openParenthesisWritten = false;
                foreach (var row in expression.Rows)
                {
                    _writer.WriteStartValues();

                    if (!openParenthesisWritten && expression.Rows.Count() > 1)
                    {
                        _writer.WriteOpenParenthesis();
                        openParenthesisWritten = true;
                    }

                    Visit(row);
                    _writer.WriteEndValues();
                }
                if (expression.Rows.Count() > 1)
                {
                    _writer.WriteCloseParenthesis();
                }
                return expression;
            }

            protected override SqlExpression VisitColumn(SqlColumn expression)
            {
                _writer.WriteColumn(expression.TableName, expression.ColumnName, expression.Alias);
                return expression;
            }

            protected override SqlExpression VisitTable(SqlTable expression)
            {
                _writer.WriteTable(expression.TableName, expression.Alias);
                return expression;
            }

            protected override SqlExpression VisitConstant(SqlConstant expression)
            {
                _writer.WriteValue(expression.Value);
                return expression;
            }

            protected override SqlExpression VisitParameter(SqlParameter expression)
            {
                _writer.WriteParameter(expression.ParameterName);

                if (ParameterVisited != null)
                {
                    ParameterVisited(expression);
                }

                return expression;
            }

            public Action<SqlParameter> ParameterVisited
            {
                get;
                set;
            }

            protected override SqlExpression VisitSort(SqlSort expression)
            {
                Visit(expression.Column);
                _writer.WriteSortOrder(expression.SortOrder);
                return expression;
            }

            protected override SqlExpression VisitBinary(SqlBinaryExpression expression)
            {
                switch (expression.Left.ExpressionType)
                {
                    case SqlExpressionType.Column:
                        Visit(expression.Left);
                        break;

                    default:
                        _writer.WriteOpenParenthesis();
                        Visit(expression.Left);
                        break;
                }

                _writer.WriteOperator(expression.Operator);

                switch (expression.Right.ExpressionType)
                {
                    case SqlExpressionType.Constant:
                    case SqlExpressionType.Parameter:
                    case SqlExpressionType.Column:
                        Visit(expression.Right);
                        break;

                    default:
                        Visit(expression.Right);
                        _writer.WriteCloseParenthesis();
                        break;
                }

                return expression;
            }

            protected override SqlExpression VisitJoin(SqlJoin expression)
            {
                _writer.WriteStartJoin(expression.Type);
                Visit(expression.Table);
                if (expression.Predicate != null)
                {
                    _writer.WriteStartOn();
                    Visit(expression.Predicate);
                }
                return expression;
            }
        }
    }
}
