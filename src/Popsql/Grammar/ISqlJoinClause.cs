namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL JOIN clause.
	/// </summary>
	public interface ISqlJoinClause : ISqlSelectFromClause
	{
		/// <summary>
		/// Adds a SQL ON clause to this SQL JOIN clause.
		/// </summary>
		/// <param name="predicate">
		/// The predicate used for determining which rows are grouped by this GROUP BY clause.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlSelectFromClause On(SqlExpression predicate);
	}
}