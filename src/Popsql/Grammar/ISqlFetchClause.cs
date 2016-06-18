namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL FETCH FIRST clause.
	/// </summary>
	/// <typeparam name="T">
	/// The type of <see cref="SqlStatement"/> that is the parent of this clause.
	/// </typeparam>
	public interface ISqlFetchClause<out T> : ISqlGo<T>
		where T : SqlStatement
	{
	}
}