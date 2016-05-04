using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlUpdate
	{
		internal class UpdateClause : OwnedBy<SqlUpdate>, ISqlUpdateClause
		{
			public UpdateClause(SqlTable table) 
				: base(new SqlUpdate(table))
			{
			}

			ISqlSetClause ISqlUpdateClause.Set(SqlColumn column, SqlValue value)
			{
				return new SetClause(Parent, column, value);
			}
		}
	}
}