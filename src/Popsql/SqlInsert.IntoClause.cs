using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlInsert
	{
		private class IntoClause : SqlClause<SqlInsert>, ISqlIntoClause
		{
			public IntoClause(SqlInsert parent) 
				: base(parent)
			{
			}

			ISqlValuesClause ISqlIntoClause.Values(params SqlValue[] values)
			{
				if (values == null || !values.Any()) throw new ArgumentNullException(nameof(values));
				if (Parent._values == null)
				{
					Parent._values = new List<IEnumerable<SqlValue>>();
				}
				Parent._values.Add(values);
				return new ValuesClause(Parent);
			}

			// We can't reach this for testing and it isn't accessed anyway.
			[ExcludeFromCodeCoverage]
			public override SqlExpressionType ExpressionType
				=> Parent.ExpressionType;
		}
	}
}