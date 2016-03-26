using System;

namespace Popsql
{
	public class SqlSubquery : SqlTableExpression
	{
		public SqlSubquery(SqlSelect query, string alias) 
			: base(alias)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (alias == null) throw new ArgumentNullException(nameof(alias));

			Query = query;
		}

		public SqlSelect Query
		{
			get;
			private set;
		}
	}
}