using System.Collections.Generic;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect
	{
		internal class SelectClause : SqlClause<SqlSelect>, ISqlSelectClause
		{
			public SelectClause(SqlSelect parent)
				: base(parent)
			{
			}

			public SelectClause(IEnumerable<SqlColumn> columns) 
				: base(new SqlSelect(columns))
			{
			}

			ISqlSelectFromClause ISqlSelectClause.From(SqlTable table)
			{
				return new FromClause(Parent, table);
			}

			ISqlSelectFromClause ISqlSelectClause.From(SqlSubquery query)
			{
				return new FromClause(Parent, query);
			}

			public override SqlExpressionType ExpressionType
				=> Parent.ExpressionType;
		}
	}
}