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
		/// <param name="predicate">
		/// The predicate used for determining which rows are joined by this JOIN clause.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlSelectFromClause Join(SqlTable table, SqlExpression predicate = null);

		/// <summary>
		/// Adds a SQL INNER JOIN clause to this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table with which to join.
		/// </param>
		/// <param name="predicate">
		/// The predicate used for determining which rows are joined by this JOIN clause.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlSelectFromClause InnerJoin(SqlTable table, SqlExpression predicate = null);

		/// <summary>
		/// Adds a SQL LEFT JOIN clause to this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table with which to join.
		/// </param>
		/// <param name="predicate">
		/// The predicate used for determining which rows are joined by this JOIN clause.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlSelectFromClause LeftJoin(SqlTable table, SqlExpression predicate = null);

		/// <summary>
		/// Adds a SQL RIGHT JOIN clause to this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table with which to join.
		/// </param>
		/// <param name="predicate">
		/// The predicate used for determining which rows are joined by this JOIN clause.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		ISqlSelectFromClause RightJoin(SqlTable table, SqlExpression predicate = null);

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