﻿using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlDelete
	{
		private class WhereClause : OwnedBy<SqlDelete>, ISqlWhereClause<SqlDelete>
		{
			public WhereClause(SqlDelete parent, SqlExpression predicate) 
				: base(parent)
			{
				Parent.Where = new SqlWhere(predicate);
			}

			public SqlDelete Go()
			{
				return Parent;
			}
		}
	}
}