using System;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL ORDER BY clause.
	/// </summary>
	/// <typeparam name="TParent">
	/// The type of <see cref="SqlStatement"/> that is the parent of this clause.
	/// </typeparam>
	public class SqlOrderBy<TParent> : SqlClause<TParent> 
		where TParent : SqlStatement
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlOrderBy{TParent}"/> class using
		/// the specified <paramref name="parent"/>.
		/// </summary>
		/// <param name="parent">
		/// The <see cref="SqlStatement"/> to which this clause belongs.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="parent"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlOrderBy(TParent parent) 
			: base(parent)
		{
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.OrderBy;
	}
}