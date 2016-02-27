namespace Popsql.Grammar
{
	public interface ISqlOrderByClause<out T> : ISqlGo<T> 
		where T : SqlStatement
	{
		ISqlOrderByClause<T> OrderBy(SqlColumn column, SqlSortOrder sortOrder = SqlSortOrder.Ascending);
		ISqlOrderByClause<T> OrderBy(SqlSort sortExpression);
	}
}