namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL ORDER BY clause.
	/// </summary>
	/// <typeparam name="T">
	/// The type of <see cref="SqlStatement"/> that is the parent of this clause.
	/// </typeparam>
	public interface ISqlOrderByClause<out T> : ISqlGo<T> 
		where T : SqlStatement
	{
		/// <summary>
		/// Sets the sort order used for sorting the results of this statement.
		/// </summary>
		/// <param name="column">
		/// The <see cref="SqlColumn"/> on which to sort.
		/// </param>
		/// <param name="sortOrder">
		/// The <see cref="SqlSortOrder"/> derermining the sorting order.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlOrderByClause<T> OrderBy(SqlColumn column, SqlSortOrder sortOrder = SqlSortOrder.Ascending);

		/// <summary>
		/// Sets the sort order used for sorting the results of this statement.
		/// </summary>
		/// <param name="sortExpression">
		/// The <see cref="SqlSort"/> determining the sort order.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlOrderByClause<T> OrderBy(SqlSort sortExpression);
	}
}