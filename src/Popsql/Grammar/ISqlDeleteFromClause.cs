namespace Popsql.Grammar
{
	public interface ISqlDeleteFromClause<out T> : ISqlFromClause<T>
		where T : SqlStatement
	{
		ISqlWhereClause<T> Where(SqlExpression expression);
	}
}