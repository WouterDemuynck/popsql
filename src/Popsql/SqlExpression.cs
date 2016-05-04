using System;
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
		/// Gets the expression type of this expression.
		/// </summary>
		public abstract SqlExpressionType ExpressionType
		{
			get;
		}

		public virtual void Accept(ISqlVisitor visitor)
		{
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));
			visitor.Visit(this);
		}
	}
}
