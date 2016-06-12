namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL SELECT FROM clause.
	/// </summary>
	public interface ISqlSelectFromClause : ISqlFromClause<SqlSelect>, ISqlOrderByClause<SqlSelect>
	{
		/// <summary>
		/// Sets the predicate used for determining which rows are selected by this SQL SELECT statement.
		/// </summary>
		/// <param name="predicate">
		/// The predicate used for determining which rows are selected by this SQL SELECT statement.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlSelectWhereClause Where(SqlExpression predicate);

		/// <summary>
		/// Adds a SQL JOIN clause to this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table with which to join.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlJoinClause Join(SqlTable table);

		/// <summary>
		/// Adds a SQL INNER JOIN clause to this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table with which to join.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlJoinClause InnerJoin(SqlTable table);

		/// <summary>
		/// Adds a SQL LEFT JOIN clause to this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table with which to join.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlJoinClause LeftJoin(SqlTable table);

		/// <summary>
		/// Adds a SQL RIGHT JOIN clause to this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table with which to join.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlJoinClause RightJoin(SqlTable table);

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