using System;
using System.Collections.Generic;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect
	{
		private class GroupByClause : OwnedBy<SqlSelect>, ISqlGroupByClause, ISqlHavingClause, ISqlThenByClause<SqlSelect>, ISqlLimitClause<SqlSelect>
		{
			public GroupByClause(SqlSelect parent, SqlColumn column)
				: base(parent)
			{
				Parent.GroupBy = new SqlGroupBy(column);
			}

			public ISqlThenByClause<SqlSelect> OrderBy(SqlColumn column, SqlSortOrder sortOrder)
			{
				if (column == null) throw new ArgumentNullException(nameof(column));

				Parent.OrderBy.Add(column + sortOrder);
				return this;
			}

			public ISqlThenByClause<SqlSelect> OrderBy(SqlSort sortExpression)
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

			public ISqlLimitClause<SqlSelect> Limit(int offset, int count)
			{
				Parent.Limit = new SqlLimit(offset, count);
				return this;
			}

			public ISqlLimitClause<SqlSelect> Limit(int count)
			{
				Parent.Limit = new SqlLimit(null, count);
				return this;
			}

			public SqlSelect Go()
			{
				return Parent;
			}
		}
	}
}