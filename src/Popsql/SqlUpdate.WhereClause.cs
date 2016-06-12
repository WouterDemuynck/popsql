using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlUpdate
	{
		private class WhereClause : OwnedBy<SqlUpdate>, ISqlWhereClause<SqlUpdate>
		{
			public WhereClause(SqlUpdate parent) 
				: base(parent)
			{
			}

			public SqlUpdate Go()
			{
				return Parent;
			}
		}
	}
}