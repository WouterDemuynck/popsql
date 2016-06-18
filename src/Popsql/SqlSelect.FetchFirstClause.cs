using System;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlSelect
	{
		private class FetchFirstClause : OwnedBy<SqlSelect>, ISqlOffsetClause<SqlSelect>, ISqlFetchClause<SqlSelect>
		{
			public FetchFirstClause(SqlSelect parent, int offset)
				: base(parent)
			{
				Parent.FetchFirst = new SqlFetchFirst(offset);
			}

			public ISqlFetchClause<SqlSelect> Fetch(int count)
			{
				if (count < 1) throw new ArgumentException(
					$"The {nameof(count)} argument must have a value greater than or equal to 1.",
					nameof(count));

				Parent.FetchFirst.Count = count;
				return this;
			}

			public SqlSelect Go()
			{
				return Parent;
			}
		}
	}
}