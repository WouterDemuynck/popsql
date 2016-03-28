using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlUpdate
	{
		private class SetClause : SqlClause<SqlUpdate>, ISqlSetClause
		{
			public SetClause(SqlUpdate parent, SqlColumn column, SqlValue value) 
				: base(parent)
			{
				((ISqlUpdateClause)this).Set(column, value);
			}

			ISqlSetClause ISqlUpdateClause.Set(SqlColumn column, SqlValue value)
			{
				if (column == null) throw new ArgumentNullException(nameof(column));
				if (value == null) value = SqlConstant.Null;

				if (Parent._values == null)
				{
					Parent._values = new List<SqlAssign>();
				}
				Parent._values.Add(new SqlAssign(column, value));
				return this;
			}

			ISqlWhereClause<SqlUpdate> ISqlSetClause.Where(SqlExpression predicate)
			{
				Parent.Where = predicate;
				return new WhereClause(Parent);
			}

			SqlUpdate ISqlGo<SqlUpdate>.Go()
			{
				return Parent;
			}

			[ExcludeFromCodeCoverage]
			public override SqlExpressionType ExpressionType
				=> Parent.ExpressionType;
		}
	}
}