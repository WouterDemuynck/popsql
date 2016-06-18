namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL OFFSET clause.
	/// </summary>
	/// <typeparam name="T">
	/// The type of <see cref="SqlStatement"/> that is the parent of this clause.
	/// </typeparam>
	public interface ISqlOffsetClause<out T> : ISqlGo<T>
		where T : SqlStatement
	{
		/// <summary>
		/// Adds a SQL FETCH FIRST clause to this SQL OFFSET clause.
		/// </summary>
		/// <param name="count">
		/// The number of rows to fetch.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlFetchClause<T> Fetch(int count);
	}
}