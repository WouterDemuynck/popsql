using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlUpdate
	{
		private class SetClause : OwnedBy<SqlUpdate>, ISqlSetClause
		{
			public SetClause(SqlUpdate parent, SqlColumn column, SqlValue value) 
				: base(parent)
			{
				Set(column, value);
			}

			public ISqlSetClause Set(SqlColumn column, SqlValue value)
			{
				if (column == null) throw new ArgumentNullException(nameof(column));
				if (value == null) value = SqlConstant.Null;

				Parent.Set.Add(new SqlAssign(column, value));
				return this;
			}

			public ISqlWhereClause<SqlUpdate> Where(SqlExpression predicate)
			{
				Parent.Where = new SqlWhere(predicate);
				return new WhereClause(Parent);
			}

			public SqlUpdate Go()
			{
				return Parent;
			}
		}
	}
}