using Popsql.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql
{
    /// <summary>
    /// Provides extension methods for writing SQL expression trees to SQL text.
    /// </summary>
    public static class SqlStatementExtensions
    {
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
            if (sql == null) throw new ArgumentNullException("sql");

            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                SqlWriterVisitor visitor = new SqlWriterVisitor(writer);
                visitor.Visit(sql);
            }

            return builder.ToString();
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
            if (sql == null) throw new ArgumentNullException("sql");

            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                SqlWriterVisitor visitor = new SqlWriterVisitor(writer);
                visitor.Visit(sql);
            }

            return builder.ToString();
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
            if (sql == null) throw new ArgumentNullException("sql");

            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                SqlWriterVisitor visitor = new SqlWriterVisitor(writer);
                visitor.Visit(sql);
            }

            return builder.ToString();
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
            if (sql == null) throw new ArgumentNullException("sql");

            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                SqlWriterVisitor visitor = new SqlWriterVisitor(writer);
                visitor.Visit(sql);
            }

            return builder.ToString();
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

                if (expression.Predicate != null)
                {
                    _writer.WriteStartWhere();
                    Visit(expression.Predicate);
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
                        Visit(expression.Right);
                        break;

                    default:
                        Visit(expression.Right);
                        _writer.WriteCloseParenthesis();
                        break;
                }

                return expression;
            }
        }
    }
}
