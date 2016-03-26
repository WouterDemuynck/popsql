using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect
	{
		private class WhereClause : SqlWhere<SqlSelect>, ISqlSelectWhereClause
		{
			public WhereClause(SqlSelect parent, SqlExpression predicate) 
				: base(parent)
			{
				Parent.Where = predicate;
			}

			public ISqlOrderByClause<SqlSelect> OrderBy(SqlSort sortExpression)
			{
				return new OrderByClause(Parent, sortExpression);
			}

			public ISqlOrderByClause<SqlSelect> OrderBy(SqlColumn column, SqlSortOrder sortOrder = SqlSortOrder.Ascending)
			{
				return new OrderByClause(Parent, column, sortOrder);
			}

			public SqlSelect Go()
			{
				return Parent;
			}
		}
	}
}