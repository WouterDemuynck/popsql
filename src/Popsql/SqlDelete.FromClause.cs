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

			public ISqlWhereClause<SqlDelete> Where(SqlExpression predicate)
			{
				Parent.Where = new SqlWhere(predicate);
				return new WhereClause(Parent, predicate);
			}

			public SqlDelete Go()
			{
				return Parent;
			}
		}
	}
}