namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL JOIN clause.
	/// </summary>
	public interface ISqlJoinClause : ISqlSelectFromClause
	{
		ISqlSelectFromClause On(SqlExpression predicate);
	}
}