using System;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL SELECT query used as a table expression.
	/// </summary>
	public class SqlSubquery : SqlTableExpression
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlSubquery"/> class using the
		/// specified <paramref name="query"/> and <paramref name="alias"/>.
		/// </summary>
		/// <param name="query">
		/// The <see cref="SqlSelect"/> query to use as an expression.
		/// </param>
		/// <param name="alias">
		/// The alias used for the subquery.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="query"/> or <paramref name="alias"/> argument is
		/// <see langword="null"/>.
		/// </exception>
		public SqlSubquery(SqlSelect query, string alias) 
			: base(alias)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (alias == null) throw new ArgumentNullException(nameof(alias));

			Query = query;
		}

		/// <summary>
		/// Gets the <see cref="SqlSelect"/> query used as table expression.
		/// </summary>
		public SqlSelect Query
		{
			get;
			private set;
		}
	}
}