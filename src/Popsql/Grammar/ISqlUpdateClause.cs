namespace Popsql.Grammar
{
	public interface ISqlUpdateClause
	{
		ISqlSetClause Set(SqlColumn column, SqlValue value);
	}
}