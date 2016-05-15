using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect
	{
		private class JoinClause : FromClause, ISqlJoinClause
		{
			private readonly FromClause _from;
			private readonly SqlJoin _join;

			public JoinClause(FromClause parent, SqlJoin join)
				: base(parent)
			{
				_from = parent;
				_join = join;
			}

			public ISqlSelectFromClause On(SqlExpression predicate)
			{
				_join.On = new SqlOn(predicate);
				return _from;
			}
		}
	}
}