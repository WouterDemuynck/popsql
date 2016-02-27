namespace Popsql.Grammar
{
	public interface ISqlDeleteClause
	{
		ISqlDeleteFromClause<SqlDelete> From(SqlTable table);
	}
}