namespace Popsql.Grammar
{
	public interface ISqlInsertClause
	{
		ISqlIntoClause Into(SqlTable table);
		ISqlIntoClause Into(SqlTable table, params SqlColumn[] columns);
	}
}