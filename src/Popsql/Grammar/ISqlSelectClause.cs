namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL SELECT clause.
	/// </summary>
	public interface ISqlSelectClause
	{
		/// <summary>
		/// Sets the table from which rows are selected by this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table from which rows are selected.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlSelectFromClause From(SqlTable table);

		/// <summary>
		/// Sets the subquery from which rows are selected by this SQL SELECT statement.
		/// </summary>
		/// <param name="query">
		/// The subquery from which rows are selected.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlSelectFromClause From(SqlSubquery query);
	}
}