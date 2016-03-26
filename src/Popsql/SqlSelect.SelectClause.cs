using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect : ISqlSelectClause
	{
		ISqlSelectFromClause ISqlSelectClause.From(SqlTable table)
		{
			return new SqlSelectFrom(this, table);
		}

		ISqlSelectFromClause ISqlSelectClause.From(SqlSubquery query)
		{
			return new SqlSelectFrom(this, query);
		}
	}
}