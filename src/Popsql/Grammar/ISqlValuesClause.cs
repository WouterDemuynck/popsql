namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL INSERT VALUES clause.
	/// </summary>
	public interface ISqlValuesClause : ISqlIntoClause, ISqlGo<SqlInsert>
	{
	}
}