using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql
{
    /// <summary>
    /// Represents a visitor or transformer for SQL expressions.
    /// </summary>
    public class SqlExpressionVisitor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlExpressionVisitor"/> class.
        /// </summary>
        public SqlExpressionVisitor()
        {
        }

        /// <summary>
        /// Dispatches the specified <paramref name="expression"/> to one of the more specialized
        /// visit methods in this class.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="SqlExpression"/> to visit.
        /// </param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original
        /// expression.
        /// </returns>
        public virtual SqlExpression Visit(SqlExpression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");

            switch (expression.ExpressionType)
            {
                case SqlExpressionType.Select:
                    return VisitSelect((SqlSelect)expression);

                case SqlExpressionType.Delete:
                    return VisitDelete((SqlDelete)expression);

                case SqlExpressionType.Update:
                    return VisitUpdate((SqlUpdate)expression);

                case SqlExpressionType.Insert:
                    return VisitInsert((SqlInsert)expression);

                case SqlExpressionType.Table:
                    return VisitTable((SqlTable)expression);

                case SqlExpressionType.Column:
                    return VisitColumn((SqlColumn)expression);

                case SqlExpressionType.Binary:
                    return VisitBinary((SqlBinaryExpression)expression);

                case SqlExpressionType.Constant:
                    return VisitConstant((SqlConstant)expression);

                case SqlExpressionType.Parameter:
                    return VisitParameter((SqlParameter)expression);
            }

            return expression;
        }

        /// <summary>
        /// Dispatches the specified <paramref name="expressions"/> to one of the more specialized
        /// visit methods in this class.
        /// </summary>
        /// <param name="expressions">
        /// The list of <see cref="SqlExpression"/> objects to visit.
        /// </param>
        /// <returns>
        /// The modified expressions, if it or any subexpression was modified; otherwise, returns the original
        /// expressions.
        /// </returns>
        public IReadOnlyCollection<SqlExpression> Visit(IEnumerable<SqlExpression> expressions)
        {
            return expressions.Select(Visit).ToArray();
        }

        /// <summary>
        /// Visits an expression, casting the result back to the original expression type.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the expression.
        /// </typeparam>
        /// <param name="expression">
        /// The expression to visit.
        /// </param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original
        /// expression.
        /// </returns>
        public T VisitAndConvert<T>(SqlExpression expression)
            where T : SqlExpression
        {
            return (T)Visit(expression);
        }

        /// <summary>
        /// Visits a collection of <paramref name="expressions"/>, casting the results back to the original expression type.
        /// </summary>
        /// <param name="expressions">
        /// The list of <see cref="SqlExpression"/> objects to visit.
        /// </param>
        /// <returns>
        /// The modified expressions, if it or any subexpression was modified; otherwise, returns the original
        /// expressions.
        /// </returns>
        public IReadOnlyCollection<T> VisitAndConvert<T>(IEnumerable<SqlExpression> expressions)
            where T : SqlExpression
        {
            return expressions.Select(Visit).Cast<T>().ToArray();
        }

        /// <summary>
        /// Visits the children of a <see cref="SqlSelect"/>.
        /// </summary>
        /// <param name="expression">
        /// The expression to visit.
        /// </param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original
        /// expression.
        /// </returns>
        protected virtual SqlExpression VisitSelect(SqlSelect expression)
        {
            SqlColumn[] columns = VisitAndConvert<SqlColumn>(expression.Columns).ToArray();
            SqlTable table = VisitAndConvert<SqlTable>(expression.Table);
            SqlExpression predicate = expression.Predicate == null ? null : Visit(expression.Predicate);

            if (!expression.Columns.SequenceEqual(columns) ||
                expression.Table != table ||
                expression.Predicate != predicate)
            {
                return Sql
                    .Select(columns)
                    .From(table)
                    .Where(predicate);
            }

            return expression;
        }

        /// <summary>
        /// Visits the children of a <see cref="SqlDelete"/>.
        /// </summary>
        /// <param name="expression">
        /// The expression to visit.
        /// </param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original
        /// expression.
        /// </returns>
        protected virtual SqlExpression VisitDelete(SqlDelete expression)
        {
            SqlTable table = VisitAndConvert<SqlTable>(expression.Table);
            SqlExpression predicate = expression.Predicate == null ? null : Visit(expression.Predicate);

            if (expression.Table != table ||
                expression.Predicate != predicate)
            {
                return Sql
                    .Delete()
                    .From(table)
                    .Where(predicate);
            }

            return expression;
        }

        /// <summary>
        /// Visits the children of a <see cref="SqlInsert"/>.
        /// </summary>
        /// <param name="expression">
        /// The expression to visit.
        /// </param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original
        /// expression.
        /// </returns>
        protected virtual SqlExpression VisitInsert(SqlInsert expression)
        {
            SqlTable table = VisitAndConvert<SqlTable>(expression.Table);
            SqlColumn[] columns = VisitAndConvert<SqlColumn>(expression.Columns).ToArray();
            IEnumerable<IEnumerable<SqlValue>> values = expression.Rows.Select(VisitAndConvert<SqlValue>);

            if (expression.Table != table ||
                !expression.Columns.SequenceEqual(columns) ||
                !expression.Rows.SequenceEqual(values))
            {
                var result = Sql
                    .Insert()
                    .Into(table, columns);

                foreach (var value in values)
                {
                    result.Values(value.ToArray());
                }

                return result;
            }

            return expression;
        }

        /// <summary>
        /// Visits the children of a <see cref="SqlUpdate"/>.
        /// </summary>
        /// <param name="expression">
        /// The expression to visit.
        /// </param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original
        /// expression.
        /// </returns>
        protected virtual SqlExpression VisitUpdate(SqlUpdate expression)
        {
            SqlTable table = VisitAndConvert<SqlTable>(expression.Table);
            SqlAssign[] values = VisitValues(expression.Values).ToArray();
            SqlExpression predicate = expression.Predicate == null ? null : Visit(expression.Predicate);

            if (expression.Table != table ||
                expression.Predicate != predicate)
            {
                var result = Sql
                    .Update(table)
                    .Where(predicate);

                foreach (var value in values)
                {
                    result.Set(value.Column, value.Value);
                }

                return result;
            }

            return expression;
        }

        private IEnumerable<SqlAssign> VisitValues(IEnumerable<SqlAssign> expressions)
        {
            foreach (var assign in expressions)
            {
                var column = VisitAndConvert<SqlColumn>(assign.Column);
                var value = VisitAndConvert<SqlValue>(assign.Value);

                if (assign.Column != column ||
                    assign.Value != value)
                {
                    yield return new SqlAssign(column, value);
                }
                else
                {
                    yield return assign;
                }
            }
        }

        /// <summary>
        /// Visits a <see cref="SqlTable"/>.
        /// </summary>
        /// <param name="expression">
        /// The expression to visit.
        /// </param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original
        /// expression.
        /// </returns>
        protected virtual SqlExpression VisitTable(SqlTable expression)
        {
            return expression;
        }

        /// <summary>
        /// Visits a <see cref="SqlColumn"/>.
        /// </summary>
        /// <param name="expression">
        /// The expression to visit.
        /// </param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original
        /// expression.
        /// </returns>
        protected virtual SqlExpression VisitColumn(SqlColumn expression)
        {
            return expression;
        }

        /// <summary>
        /// Visits a <see cref="SqlConstant"/>.
        /// </summary>
        /// <param name="expression">
        /// The expression to visit.
        /// </param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original
        /// expression.
        /// </returns>
        protected virtual SqlExpression VisitConstant(SqlConstant expression)
        {
            return expression;
        }

        /// <summary>
        /// Visits a <see cref="SqlParameter"/>.
        /// </summary>
        /// <param name="expression">
        /// The expression to visit.
        /// </param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original
        /// expression.
        /// </returns>
        protected virtual SqlExpression VisitParameter(SqlParameter expression)
        {
            return expression;
        }

        /// <summary>
        /// Visits the children of a <see cref="SqlBinaryExpression"/>.
        /// </summary>
        /// <param name="expression">
        /// The expression to visit.
        /// </param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original
        /// expression.
        /// </returns>
        protected virtual SqlExpression VisitBinary(SqlBinaryExpression expression)
        {
            SqlExpression left = Visit(expression.Left);
            SqlBinaryOperator @operator = VisitOperator(expression.Operator);
            SqlExpression right = Visit(expression.Right);

            if (expression.Left != left ||
                expression.Operator != @operator ||
                expression.Right != right)
            {
                return new SqlBinaryExpression(left, @operator, right);
            }

            return expression;
        }

        /// <summary>
        /// Visits a <see cref="SqlBinaryOperator"/>.
        /// </summary>
        /// <param name="operator">
        /// The operator to visit.
        /// </param>
        /// <returns>
        /// The modified operator, if it was modified; otherwise, returns the original
        /// operator.
        /// </returns>
        protected virtual SqlBinaryOperator VisitOperator(SqlBinaryOperator @operator)
        {
            return @operator;
        }
    }
}
