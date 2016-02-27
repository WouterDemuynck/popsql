namespace Popsql.Grammar
{
	public interface ISqlSelectClause : ISqlGo<SqlSelect>
	{
		ISqlSelectFromClause From(SqlTable table);
	}
}