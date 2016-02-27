namespace Popsql.Grammar
{
	public interface ISqlSetClause : ISqlUpdateClause, ISqlGo<SqlUpdate>
	{
		ISqlWhereClause<SqlUpdate> Where(SqlExpression predicate);
	}
}