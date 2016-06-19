using System;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect
	{
		private class OrderByClause : OwnedBy<SqlSelect>, ISqlThenByClause<SqlSelect>, ISqlLimitClause<SqlSelect>
		{
			public OrderByClause(SqlSelect parent, SqlColumn column, SqlSortOrder sortOrder)
				: base(parent)
			{
				OrderBy(column, sortOrder);
			}

			public OrderByClause(SqlSelect parent, SqlSort sortExpression)
				: base(parent)
			{
				OrderBy(sortExpression);
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