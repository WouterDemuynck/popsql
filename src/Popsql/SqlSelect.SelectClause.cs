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

			public ISqlSelectFromClause From(SqlTable table)
			{
				return new FromClause(Parent, table);
			}

			public ISqlSelectFromClause From(SqlSubquery query)
			{
				return new FromClause(Parent, query);
			}
		}
	}
}