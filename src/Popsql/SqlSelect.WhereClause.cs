using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect
	{
		private class WhereClause : OwnedBy<SqlSelect>, ISqlSelectWhereClause
		{
			public WhereClause(SqlSelect parent, SqlExpression predicate) 
				: base(parent)
			{
				Parent.Where = new SqlWhere(predicate);
			}

			public ISqlOrderByClause<SqlSelect> OrderBy(SqlSort sortExpression)
			{
				return new OrderByClause(Parent, sortExpression);
			}

			public ISqlOrderByClause<SqlSelect> OrderBy(SqlColumn column, SqlSortOrder sortOrder = SqlSortOrder.Ascending)
			{
				return new OrderByClause(Parent, column, sortOrder);
			}

			public ISqlGroupByClause GroupBy(SqlColumn column)
			{
				return new GroupByClause(Parent, column);
			}

			public SqlSelect Go()
			{
				return Parent;
			}
		}
	}
}