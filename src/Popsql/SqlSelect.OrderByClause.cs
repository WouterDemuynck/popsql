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
				OrderBy(column, sortOrder);
			}

			public OrderByClause(SqlSelect parent, SqlSort sortExpression)
				: base(parent)
			{
				OrderBy(sortExpression);
			}

			public ISqlOrderByClause<SqlSelect> OrderBy(SqlColumn column, SqlSortOrder sortOrder)
			{
				if (column == null) throw new ArgumentNullException(nameof(column));

				Parent.OrderBy.Add(column + sortOrder);
				return this;
			}

			public ISqlOrderByClause<SqlSelect> OrderBy(SqlSort sortExpression)
			{
				if (sortExpression == null) throw new ArgumentNullException(nameof(sortExpression));

				Parent.OrderBy.Add(sortExpression);
				return this;
			}

			public SqlSelect Go()
			{
				return Parent;
			}
		}
	}
}