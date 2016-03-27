using System;
using System.Collections.Generic;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect
	{
		private class FromClause : SqlFrom<SqlSelect>, ISqlSelectFromClause
		{
			public FromClause(SqlSelect parent, SqlTable table)
				: base(parent)
			{
				if (table == null) throw new ArgumentNullException(nameof(table));
				Parent.From = table;
			}

			public FromClause(SqlSelect parent, SqlSubquery query)
				: base(parent)
			{
				if (query == null) throw new ArgumentNullException(nameof(query));
				Parent.From = query;
			}

			ISqlOrderByClause<SqlSelect> ISqlOrderByClause<SqlSelect>.OrderBy(SqlColumn column, SqlSortOrder sortOrder)
			{
				return new OrderByClause(Parent, column, sortOrder);
			}

			ISqlOrderByClause<SqlSelect> ISqlOrderByClause<SqlSelect>.OrderBy(SqlSort sortExpression)
			{
				return new OrderByClause(Parent, sortExpression);
			}

			ISqlSelectWhereClause ISqlSelectFromClause.Where(SqlExpression predicate)
			{
				return new WhereClause(Parent, predicate);
			}

			ISqlSelectFromClause ISqlSelectFromClause.Join(SqlTable table, SqlExpression predicate)
			{
				return JoinInternal(SqlJoinType.Default, table, predicate);
			}

			ISqlSelectFromClause ISqlSelectFromClause.InnerJoin(SqlTable table, SqlExpression predicate)
			{
				return JoinInternal(SqlJoinType.Inner, table, predicate);
			}

			ISqlSelectFromClause ISqlSelectFromClause.LeftJoin(SqlTable table, SqlExpression predicate)
			{
				return JoinInternal(SqlJoinType.Left, table, predicate);
			}

			ISqlSelectFromClause ISqlSelectFromClause.RightJoin(SqlTable table, SqlExpression predicate)
			{
				return JoinInternal(SqlJoinType.Right, table, predicate);
			}

			private ISqlSelectFromClause JoinInternal(SqlJoinType type, SqlTable table, SqlExpression predicate = null)
			{
				if (table == null) throw new ArgumentNullException(nameof(table));
				if (Parent._joins == null)
				{
					Parent._joins = new List<SqlJoin>();
				}
				Parent._joins.Add(new SqlJoin(type, table, predicate));
				return this;
			}

			SqlSelect ISqlGo<SqlSelect>.Go()
			{
				return Parent;
			}
		}
	}
}