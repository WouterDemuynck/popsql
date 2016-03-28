namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the gramamr for the SQL UPDATE SET clause.
	/// </summary>
	public interface ISqlSetClause : ISqlUpdateClause, ISqlGo<SqlUpdate>
	{
		/// <summary>
		/// Sets the predicate used for determining which rows are updated by this SQL UPDATE statement.
		/// </summary>
		/// <param name="predicate">
		/// The predicate used for determining which rows are updated by this SQL UPDATE statement.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlWhereClause<SqlUpdate> Where(SqlExpression predicate);
	}
}