namespace Popsql.Grammar
{
	public interface ISqlIntoClause
	{
		ISqlValuesClause Values(params SqlValue[] values);
	}
}