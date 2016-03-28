namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL SELECT WHERE clause.
	/// </summary>
	public interface ISqlSelectWhereClause : ISqlWhereClause<SqlSelect>, ISqlOrderByClause<SqlSelect>
	{
	}
}