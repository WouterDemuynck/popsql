using System;
using System.Collections.Generic;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect
	{
		private class GroupByClause : OwnedBy<SqlSelect>, ISqlGroupByClause, ISqlHavingClause
		{
			public GroupByClause(SqlSelect parent, SqlColumn column)
				: base(parent)
			{
				Parent.GroupBy = new SqlGroupBy(column);
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

			public ISqlHavingClause Having(SqlExpression predicate)
			{
				Parent.GroupBy.Having = new SqlHaving(predicate);
				return this;
			}

			SqlSelect ISqlGo<SqlSelect>.Go()
			{
				return Parent;
			}
		}
	}
}