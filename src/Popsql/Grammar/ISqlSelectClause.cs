namespace Popsql.Grammar
{
	public interface ISqlSelectClause
	{
		ISqlSelectFromClause From(SqlTable table);
		ISqlSelectFromClause From(SqlSubquery query);
	}
}