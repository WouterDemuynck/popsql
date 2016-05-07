namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL SELECT WHERE clause.
	/// </summary>
	public interface ISqlSelectWhereClause : ISqlWhereClause<SqlSelect>, ISqlOrderByClause<SqlSelect>
	{
		/// <summary>
		/// Sets the column used for grouping the results of this statement.
		/// </summary>
		/// <param name="column">
		/// The <see cref="SqlColumn"/> on which to group.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlGroupByClause GroupBy(SqlColumn column);
	}
}