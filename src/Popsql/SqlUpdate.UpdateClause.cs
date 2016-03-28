using System;
using System.Collections.Generic;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlUpdate : ISqlUpdateClause
	{
		ISqlSetClause ISqlUpdateClause.Set(SqlColumn column, SqlValue value)
		{
			ISqlSetClause clause = new SetClause(this);
			clause.Set(column, value);
			return clause;
		}
	}
}