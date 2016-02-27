namespace Popsql.Grammar
{
	public interface ISqlSelectFromClause : ISqlFromClause<SqlSelect>, ISqlOrderByClause<SqlSelect>
	{
		ISqlSelectWhereClause Where(SqlExpression expression);
		ISqlSelectFromClause Join(SqlTable table, SqlExpression predicate = null);
		ISqlSelectFromClause InnerJoin(SqlTable table, SqlExpression predicate = null);
		ISqlSelectFromClause LeftJoin(SqlTable table, SqlExpression predicate = null);
		ISqlSelectFromClause RightJoin(SqlTable table, SqlExpression predicate = null);
	}
}