using System;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlDelete : ISqlDeleteClause
	{
		ISqlDeleteFromClause ISqlDeleteClause.From(SqlTable table)
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			From = table;
			return new FromClause(this);
		}
	}
}