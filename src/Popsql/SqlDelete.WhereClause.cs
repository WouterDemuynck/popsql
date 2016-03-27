using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlDelete
	{
		private class WhereClause : SqlWhere<SqlDelete>, ISqlWhereClause<SqlDelete>
		{
			public WhereClause(SqlDelete parent, SqlExpression predicate) 
				: base(parent)
			{
				Parent.Where = predicate;
			}

			SqlDelete ISqlGo<SqlDelete>.Go()
			{
				return Parent;
			}
		}
	}
}