using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlInsert
	{
		private class ValuesClause : IntoClause, ISqlValuesClause
		{
			public ValuesClause(SqlInsert parent) 
				: base(parent)
			{
			}

			SqlInsert ISqlGo<SqlInsert>.Go()
			{
				return Parent;
			}
		}
	}
}