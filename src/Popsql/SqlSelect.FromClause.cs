using System;
using System.Collections.Generic;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect
	{
		private class FromClause : OwnedBy<SqlSelect>, ISqlSelectFromClause
		{
			public FromClause(SqlSelect parent, SqlTable table)
				: base(parent)
			{
				if (table == null) throw new ArgumentNullException(nameof(table));
				Parent.From = new SqlFrom(table);
			}

			public FromClause(SqlSelect parent, SqlSubquery query)
				: base(parent)
			{
				if (query == null) throw new ArgumentNullException(nameof(query));
				if (query.Alias == null) throw new ArgumentException("An alias is required for subqueries in a FROM clause.", nameof(query));
				Parent.From = new SqlFrom(query);
			}

			protected FromClause(FromClause parent) 
				: base(parent.Parent)
			{
			}

			public ISqlOrderByClause<SqlSelect> OrderBy(SqlColumn column, SqlSortOrder sortOrder)
			{
				return new OrderByClause(Parent, column, sortOrder);
			}

			public ISqlOrderByClause<SqlSelect> OrderBy(SqlSort sortExpression)
			{
				return new OrderByClause(Parent, sortExpression);
			}

			public ISqlSelectWhereClause Where(SqlExpression predicate)
			{
				return new WhereClause(Parent, predicate);
			}

			public ISqlJoinClause Join(SqlTable table)
			{
				return JoinInternal(SqlJoinType.Default, table);
			}

			public ISqlJoinClause InnerJoin(SqlTable table)
			{
				return JoinInternal(SqlJoinType.Inner, table);
			}

			public ISqlJoinClause LeftJoin(SqlTable table)
			{
				return JoinInternal(SqlJoinType.Left, table);
			}

			public ISqlJoinClause RightJoin(SqlTable table)
			{
				return JoinInternal(SqlJoinType.Right, table);
			}

			public ISqlGroupByClause GroupBy(SqlColumn column)
			{
				return new GroupByClause(Parent, column);
			}

			private ISqlJoinClause JoinInternal(SqlJoinType type, SqlTable table, SqlExpression predicate = null)
			{
				if (table == null) throw new ArgumentNullException(nameof(table));
				var join = new SqlJoin(type, table);
				Parent.From.AddJoin(join);
				return new JoinClause(this, join);
			}

			public SqlSelect Go()
			{
				return Parent;
			}
		}
	}
}