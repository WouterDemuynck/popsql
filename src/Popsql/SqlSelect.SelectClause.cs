using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect
	{
		internal class SelectClause : OwnedBy<SqlSelect>, ISqlSelectClause
		{
			public SelectClause(SqlValue[] columns) 
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
		}
	}
}