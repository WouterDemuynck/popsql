namespace Popsql.Grammar
{
	public interface ISqlFromClause<out T> : ISqlGo<T> 
		where T : SqlStatement
	{
	}
}