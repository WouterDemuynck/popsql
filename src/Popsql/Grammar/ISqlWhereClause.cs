namespace Popsql.Grammar
{
	/// <summary>
	/// Provides grammar for the SQL WHERE clause.
	/// </summary>
	/// <typeparam name="T">
	/// The type of <see cref="SqlStatement"/> that is the parent of this clause.
	/// </typeparam>
	public interface ISqlWhereClause<out T> : ISqlGo<T> 
		where T : SqlStatement
	{
	}
}