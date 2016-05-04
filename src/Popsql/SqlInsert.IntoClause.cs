using System;
using System.Linq;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlInsert
	{
		private class IntoClause : OwnedBy<SqlInsert>, ISqlIntoClause
		{
			public IntoClause(SqlInsert parent) 
				: base(parent)
			{
			}

			ISqlValuesClause ISqlIntoClause.Values(params SqlValue[] values)
			{
				if (values == null || !values.Any()) throw new ArgumentNullException(nameof(values));

				Parent.Values.Add(values);
				return new ValuesClause(Parent);
			}
		}
	}
}