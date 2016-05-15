using System;
using Popsql.Grammar;

namespace Popsql
{
	public partial class SqlDelete
	{
		internal class DeleteClause : OwnedBy<SqlDelete>, ISqlDeleteClause
		{
			public DeleteClause() 
				: base(new SqlDelete())
			{
			}

			public ISqlDeleteFromClause From(SqlTable table)
			{
				if (table == null) throw new ArgumentNullException(nameof(table));
				Parent.From = new SqlFrom(table);
				return new FromClause(Parent);
			}
		}
	}
}