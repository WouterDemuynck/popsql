using System;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlInsert : ISqlInsertClause
	{
		ISqlIntoClause ISqlInsertClause.Into(SqlTable table)
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			Into = table;
			return new IntoClause(this);
		}

		ISqlIntoClause ISqlInsertClause.Into(SqlTable table, params SqlColumn[] columns)
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			Into = table;
			Columns = columns;
			return new IntoClause(this);
		}
	}
}