using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlDelete
	{
		private class FromClause : OwnedBy<SqlDelete>, ISqlDeleteFromClause
		{
			public FromClause(SqlDelete parent) 
				: base(parent)
			{
			}

			ISqlWhereClause<SqlDelete> ISqlDeleteFromClause.Where(SqlExpression predicate)
			{
				Parent.Where = new SqlWhere(predicate);
				return new WhereClause(Parent, predicate);
			}

			SqlDelete ISqlGo<SqlDelete>.Go()
			{
				return Parent;
			}
		}
	}
}