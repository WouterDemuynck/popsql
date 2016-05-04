using System;
using System.Collections.Generic;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect
	{
		private class OrderByClause : OwnedBy<SqlSelect>, ISqlOrderByClause<SqlSelect>
		{
			public OrderByClause(SqlSelect parent, SqlColumn column, SqlSortOrder sortOrder)
				: base(parent)
			{
				((ISqlOrderByClause<SqlSelect>)this).OrderBy(column, sortOrder);
			}

			public OrderByClause(SqlSelect parent, SqlSort sortExpression)
				: base(parent)
			{
				((ISqlOrderByClause<SqlSelect>) this).OrderBy(sortExpression);
			}

			ISqlOrderByClause<SqlSelect> ISqlOrderByClause<SqlSelect>.OrderBy(SqlColumn column, SqlSortOrder sortOrder)
			{
				if (column == null) throw new ArgumentNullException(nameof(column));

				Parent.OrderBy.Add(column + sortOrder);
				return this;
			}

			ISqlOrderByClause<SqlSelect> ISqlOrderByClause<SqlSelect>.OrderBy(SqlSort sortExpression)
			{
				if (sortExpression == null) throw new ArgumentNullException(nameof(sortExpression));

				Parent.OrderBy.Add(sortExpression);
				return this;
			}

			SqlSelect ISqlGo<SqlSelect>.Go()
			{
				return Parent;
			}
		}
	}
}