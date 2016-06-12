namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL DELETE FROM clause.
	/// </summary>
	public interface ISqlDeleteFromClause : ISqlFromClause<SqlDelete>
	{
		/// <summary>
		/// Sets the predicate used for determining which rows are deleted by this SQL DELETE statement.
		/// </summary>
		/// <param name="predicate">
		/// The predicate used for determining which rows are deleted by this SQL DELETE statement.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlWhereClause<SqlDelete> Where(SqlExpression predicate);
	}
}