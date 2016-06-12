using System;
using System.Diagnostics.CodeAnalysis;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlInsert
	{
		internal class InsertClause : OwnedBy<SqlInsert>, ISqlInsertClause
		{
			public InsertClause()
				: base(new SqlInsert())
			{
			}

			public ISqlIntoClause Into(SqlTable table)
			{
				if (table == null) throw new ArgumentNullException(nameof(table));
				Parent.Into = new SqlInto(table);
				return new IntoClause(Parent);
			}

			public ISqlIntoClause Into(SqlTable table, params SqlColumn[] columns)
			{
				if (table == null) throw new ArgumentNullException(nameof(table));
				Parent.Into = new SqlInto(table);
				Parent.Columns = columns;
				return new IntoClause(Parent);
			}
		}
	}
}