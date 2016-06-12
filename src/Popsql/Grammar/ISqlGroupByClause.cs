namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL GROUP BY clause.
	/// </summary>
	public interface ISqlGroupByClause : ISqlOrderByClause<SqlSelect>
	{
		/// <summary>
		/// Adds a SQL HAVING clause to this SQL GROUP BY clause.
		/// </summary>
		/// <param name="predicate">
		/// The predicate used for determining which rows are grouped by this GROUP BY clause.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlHavingClause Having(SqlExpression predicate);
	}
}