using System;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL WHERE statement beloning to a specific SQL statement.
	/// </summary>
	/// <typeparam name="TParent">
	/// The type of <see cref="SqlStatement"/> that is the parent of this clause.
	/// </typeparam>
	public class SqlWhere<TParent> : SqlClause<TParent> 
		where TParent : SqlStatement
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlWhere{TParent}"/> class
		/// using the specified parent statement.
		/// </summary>
		/// <param name="parent">
		/// The <see cref="SqlStatement"/> to which this clause belongs.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="parent"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlWhere(TParent parent) 
			: base(parent)
		{
		}

		/// <summary>
		/// Returns the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Where;
	}
}