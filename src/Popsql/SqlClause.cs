using System;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL statement clause.
	/// </summary>
	/// <typeparam name="TParent">
	/// The type of the parent SQL statement to which this clause belongs.
	/// </typeparam>
	public abstract class SqlClause<TParent> : SqlExpression
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlClause{TParent}"/> class using
		/// the specified <paramref name="parent"/> statement.
		/// </summary>
		/// <param name="parent">
		/// The <see cref="SqlStatement"/> that is the parent of the new 
		/// <see cref="SqlClause{TParent}"/> object..
		/// </param>
		protected SqlClause(TParent parent)
		{
			if (parent == null) throw new ArgumentNullException(nameof(parent));
			Parent = parent;
		}

		/// <summary>
		/// Gets the parent <see cref="SqlStatement"/> of this <see cref="SqlClause{TParent}"/>.
		/// </summary>
		protected TParent Parent
		{
			get;
			private set;
		}
	}
}