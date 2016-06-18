namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL ORDER BY clause.
	/// </summary>
	/// <typeparam name="T">
	/// The type of <see cref="SqlStatement"/> that is the parent of this clause.
	/// </typeparam>
	public interface ISqlThenByClause<out T> : ISqlOrderByClause<T>
		where T : SqlStatement
	{
		/// <summary>
		/// Adds a SQL OFFSET clause to this SQL OFFSET clause.
		/// </summary>
		/// <param name="offset">
		/// The offset of the first row to fetch.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlOffsetClause<T> Offset(int offset);
	}
}