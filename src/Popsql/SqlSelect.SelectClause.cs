using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect : ISqlSelectClause
	{
		ISqlSelectFromClause ISqlSelectClause.From(SqlTable table)
		{
			return new FromClause(this, table);
		}

		ISqlSelectFromClause ISqlSelectClause.From(SqlSubquery query)
		{
			return new FromClause(this, query);
		}
	}
}