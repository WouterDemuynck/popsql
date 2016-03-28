using System;
using System.Diagnostics.CodeAnalysis;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlDelete
	{
		internal class DeleteClause : SqlClause<SqlDelete>, ISqlDeleteClause
		{
			public DeleteClause() 
				: base(new SqlDelete())
			{
			}

			ISqlDeleteFromClause ISqlDeleteClause.From(SqlTable table)
			{
				if (table == null) throw new ArgumentNullException(nameof(table));
				Parent.From = table;
				return new FromClause(Parent);
			}

			[ExcludeFromCodeCoverage]
			public override SqlExpressionType ExpressionType
				=> Parent.ExpressionType;
		}
	}
}