using System;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect
	{
		private class OrderByClause : OwnedBy<SqlSelect>, ISqlThenByClause<SqlSelect>
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

			public SqlSelect Go()
			{
				return Parent;
			}

			public ISqlOffsetClause<SqlSelect> Offset(int offset)
			{
				return new FetchFirstClause(Parent, offset);
			}
		}
	}
}