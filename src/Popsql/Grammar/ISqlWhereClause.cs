namespace Popsql.Grammar
{
	public interface ISqlWhereClause<out T> : ISqlGo<T> 
		where T : SqlStatement
	{
	}
}