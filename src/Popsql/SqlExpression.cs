using System;
using System.Linq;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Provides the base class for SQL expressions and provides <see langword="static"/> factory methods
	/// for creating specific SQL expressions.
	/// </summary>
	public abstract class SqlExpression : ISqlVisitable
	{
		/// <summary>
		/// Creates a <see cref="SqlCast"/> that represents a conversion of the specified <paramref name="value"/>
		/// to the specified data type.
		/// </summary>
		/// <param name="value">
		/// The <see cref="SqlValue"/> to convert.
		/// </param>
		/// <param name="dataType">
		/// The <see cref="SqlDataType"/> to convert to.
		/// </param>
		/// <returns>
		/// A <see cref="SqlCast"/> that represents a conversion of the specified <paramref name="value"/>
		/// to the specified data type.
		/// </returns>
		public static SqlCast Cast(SqlValue value, SqlDataType dataType)
		{
			return new SqlCast(value, dataType);
		}

		/// <summary>
		/// Creates a <see cref="SqlBinaryExpression"/> that represents a logical <b>AND</b> operation.
		/// </summary>
		/// <param name="left">
		/// A <see cref="SqlBinaryExpression"/> to use as the left operand in the expression.
		/// </param>
		/// <param name="right">
		/// A <see cref="SqlBinaryExpression"/> to use as the right operand in the expression.
		/// </param>
		/// <returns>
		/// A <see cref="SqlBinaryExpression"/> that represents a logical <b>AND</b> operation between
		/// the specified <paramref name="left"/> and <paramref name="right"/> operands.
		/// </returns>
		public static SqlBinaryExpression And(SqlBinaryExpression left, SqlBinaryExpression right)
		{
			return new SqlBinaryExpression(left, SqlBinaryOperator.And, right);
		}

		/// <summary>
		/// Creates a <see cref="SqlBinaryExpression"/> that represents a logical <b>OR</b> operation.
		/// </summary>
		/// <param name="left">
		/// A <see cref="SqlBinaryExpression"/> to use as the left operand in the expression.
		/// </param>
		/// <param name="right">
		/// A <see cref="SqlBinaryExpression"/> to use as the right operand in the expression.
		/// </param>
		/// <returns>
		/// A <see cref="SqlBinaryExpression"/> that represents a logical <b>OR</b> operation between
		/// the specified <paramref name="left"/> and <paramref name="right"/> operands.
		/// </returns>
		public static SqlBinaryExpression Or(SqlBinaryExpression left, SqlBinaryExpression right)
		{
			return new SqlBinaryExpression(left, SqlBinaryOperator.Or, right);
		}

		/// <summary>
		/// Creates a <see cref="SqlBinaryExpression"/> that represents an equality comparison.
		/// </summary>
		/// <param name="column">
		/// A <see cref="SqlColumn"/> to use as the left operand in the expression.
		/// </param>
		/// <param name="value">
		/// A <see cref="SqlValue"/> to use as the right operand in the expression.
		/// </param>
		/// <returns>
		/// A <see cref="SqlBinaryExpression"/> that represents an equality comparison between
		/// the value of the specified <paramref name="column"/> and the specified <paramref name="value"/>.
		/// </returns>
		/// <remarks>
		/// If you pass <see langword="null"/> for the <paramref name="value"/> argument, it
		/// will be automatically converted to <see cref="SqlConstant.Null"/>.
		/// </remarks>
		public static SqlBinaryExpression Equal(SqlColumn column, SqlValue value)
		{
			return new SqlBinaryExpression(column, SqlBinaryOperator.Equal, value ?? SqlConstant.Null);
		}

		/// <summary>
		/// Creates a <see cref="SqlBinaryExpression"/> that represents an inequality comparison.
		/// </summary>
		/// <param name="column">
		/// A <see cref="SqlColumn"/> to use as the left operand in the expression.
		/// </param>
		/// <param name="value">
		/// A <see cref="SqlValue"/> to use as the right operand in the expression.
		/// </param>
		/// <returns>
		/// A <see cref="SqlBinaryExpression"/> that represents an inequality comparison between
		/// the value of the specified <paramref name="column"/> and the specified <paramref name="value"/>.
		/// </returns>
		/// <remarks>
		/// If you pass <see langword="null"/> for the <paramref name="value"/> argument, it
		/// will be automatically converted to <see cref="SqlConstant.Null"/>.
		/// </remarks>
		public static SqlBinaryExpression NotEqual(SqlColumn column, SqlValue value)
		{
			return new SqlBinaryExpression(column, SqlBinaryOperator.NotEqual, value ?? SqlConstant.Null);
		}

		/// <summary>
		/// Creates a <see cref="SqlBinaryExpression"/> that represents an "greater than" comparison.
		/// </summary>
		/// <param name="column">
		/// A <see cref="SqlColumn"/> to use as the left operand in the expression.
		/// </param>
		/// <param name="value">
		/// A <see cref="SqlValue"/> to use as the right operand in the expression.
		/// </param>
		/// <returns>
		/// A <see cref="SqlBinaryExpression"/> that represents an "greater than" comparison between
		/// the value of the specified <paramref name="column"/> and the specified <paramref name="value"/>.
		/// </returns>
		/// <remarks>
		/// If you pass <see langword="null"/> for the <paramref name="value"/> argument, it
		/// will be automatically converted to <see cref="SqlConstant.Null"/>.
		/// </remarks>
		public static SqlBinaryExpression GreaterThan(SqlColumn column, SqlValue value)
		{
			return new SqlBinaryExpression(column, SqlBinaryOperator.GreaterThan, value ?? SqlConstant.Null);
		}

		/// <summary>
		/// Creates a <see cref="SqlBinaryExpression"/> that represents an "greater than or equal to" comparison.
		/// </summary>
		/// <param name="column">
		/// A <see cref="SqlColumn"/> to use as the left operand in the expression.
		/// </param>
		/// <param name="value">
		/// A <see cref="SqlValue"/> to use as the right operand in the expression.
		/// </param>
		/// <returns>
		/// A <see cref="SqlBinaryExpression"/> that represents an "greater than or equal to" comparison between
		/// the value of the specified <paramref name="column"/> and the specified <paramref name="value"/>.
		/// </returns>
		/// <remarks>
		/// If you pass <see langword="null"/> for the <paramref name="value"/> argument, it
		/// will be automatically converted to <see cref="SqlConstant.Null"/>.
		/// </remarks>
		public static SqlBinaryExpression GreaterThanOrEqual(SqlColumn column, SqlValue value)
		{
			return new SqlBinaryExpression(column, SqlBinaryOperator.GreaterThanOrEqual, value ?? SqlConstant.Null);
		}

		/// <summary>
		/// Creates a <see cref="SqlBinaryExpression"/> that represents an "less than" comparison.
		/// </summary>
		/// <param name="column">
		/// A <see cref="SqlColumn"/> to use as the left operand in the expression.
		/// </param>
		/// <param name="value">
		/// A <see cref="SqlValue"/> to use as the right operand in the expression.
		/// </param>
		/// <returns>
		/// A <see cref="SqlBinaryExpression"/> that represents an "less than" comparison between
		/// the value of the specified <paramref name="column"/> and the specified <paramref name="value"/>.
		/// </returns>
		/// <remarks>
		/// If you pass <see langword="null"/> for the <paramref name="value"/> argument, it
		/// will be automatically converted to <see cref="SqlConstant.Null"/>.
		/// </remarks>
		public static SqlBinaryExpression LessThan(SqlColumn column, SqlValue value)
		{
			return new SqlBinaryExpression(column, SqlBinaryOperator.LessThan, value ?? SqlConstant.Null);
		}

		/// <summary>
		/// Creates a <see cref="SqlBinaryExpression"/> that represents an "less than or equal to" comparison.
		/// </summary>
		/// <param name="column">
		/// A <see cref="SqlColumn"/> to use as the left operand in the expression.
		/// </param>
		/// <param name="value">
		/// A <see cref="SqlValue"/> to use as the right operand in the expression.
		/// </param>
		/// <returns>
		/// A <see cref="SqlBinaryExpression"/> that represents an "less than or equal to" comparison between
		/// the value of the specified <paramref name="column"/> and the specified <paramref name="value"/>.
		/// </returns>
		/// <remarks>
		/// If you pass <see langword="null"/> for the <paramref name="value"/> argument, it
		/// will be automatically converted to <see cref="SqlConstant.Null"/>.
		/// </remarks>
		public static SqlBinaryExpression LessThanOrEqual(SqlColumn column, SqlValue value)
		{
			return new SqlBinaryExpression(column, SqlBinaryOperator.LessThanOrEqual, value ?? SqlConstant.Null);
		}

		/// <summary>
		/// Creates a <see cref="SqlBinaryExpression"/> that represents an "like" comparison.
		/// </summary>
		/// <param name="column">
		/// A <see cref="SqlColumn"/> to use as the left operand in the expression.
		/// </param>
		/// <param name="value">
		/// A <see cref="SqlValue"/> to use as the right operand in the expression.
		/// </param>
		/// <returns>
		/// A <see cref="SqlBinaryExpression"/> that represents an "like" comparison between
		/// the value of the specified <paramref name="column"/> and the specified <paramref name="value"/>.
		/// </returns>
		/// <remarks>
		/// If you pass <see langword="null"/> for the <paramref name="value"/> argument, it
		/// will be automatically converted to <see cref="SqlConstant.Null"/>.
		/// </remarks>
		public static SqlBinaryExpression Like(SqlColumn column, SqlValue value)
		{
			return new SqlBinaryExpression(column, SqlBinaryOperator.Like, value ?? SqlConstant.Null);
		}

		/// <summary>
		/// Creates a <see cref="SqlBinaryExpression"/> that represents an "in" comparison.
		/// </summary>
		/// <param name="column">
		/// A <see cref="SqlColumn"/> to use as the left operand in the expression.
		/// </param>
		/// <param name="values">
		/// A collection of <see cref="SqlValue"/> to use as the right operand in the expression.
		/// </param>
		/// <returns>
		/// A <see cref="SqlBinaryExpression"/> that represents an "in" comparison between
		/// the value of the specified <paramref name="column"/> and the specified collection of
		/// <paramref name="values"/>.
		/// </returns>
		/// <remarks>
		/// If you pass <see langword="null"/> for the <paramref name="values"/> argument, it
		/// will be automatically converted to an empty collection.
		/// </remarks>
		public static SqlBinaryExpression In(SqlColumn column, params SqlValue[] values)
		{
			return new SqlBinaryExpression(column, SqlBinaryOperator.In, new SqlValueList(values ?? Enumerable.Empty<SqlValue>()));
		}

		/// <summary>
		/// Creates a <see cref="SqlBinaryExpression"/> that represents an "in" comparison.
		/// </summary>
		/// <param name="column">
		/// A <see cref="SqlColumn"/> to use as the left operand in the expression.
		/// </param>
		/// <param name="query">
		/// A <see cref="SqlSubquery"/> to use as the right operand in the expression.
		/// </param>
		/// <returns>
		/// A <see cref="SqlBinaryExpression"/> that represents an "in" comparison between
		/// the value of the specified <paramref name="column"/> and the specified <paramref name="query"/>.
		/// </returns>
		public static SqlBinaryExpression In(SqlColumn column, SqlSubquery query)
		{
			return new SqlBinaryExpression(column, SqlBinaryOperator.In, query);
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public abstract SqlExpressionType ExpressionType
		{
			get;
		}

		/// <summary>
		/// Accepts the specified <paramref name="visitor"/> and dispatches calls to the specific visitor
		/// methods for this object.
		/// </summary>
		/// <param name="visitor">
		/// The <see cref="ISqlVisitor" /> to visit this object with.
		/// </param>
		public virtual void Accept(ISqlVisitor visitor)
		{
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));
			visitor.Visit(this);
		}
	}
}
