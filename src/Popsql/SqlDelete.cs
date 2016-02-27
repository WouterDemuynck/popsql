using System;
using Popsql.Grammar;

namespace Popsql
{
	public class SqlDelete : SqlStatement, ISqlDeleteClause, ISqlDeleteFromClause<SqlDelete>, ISqlWhereClause<SqlDelete>
	{
		public override SqlExpressionType ExpressionType
		{
			get
			{
				return SqlExpressionType.Delete;
			}
		}

		public SqlTable From
		{
			get;
			private set;
		}

		public SqlExpression Where
		{
			get;
			private set;
		}

		ISqlDeleteFromClause<SqlDelete> ISqlDeleteClause.From(SqlTable table)
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			From = table;
			return this;
		}

		ISqlWhereClause<SqlDelete> ISqlDeleteFromClause<SqlDelete>.Where(SqlExpression predicate)
		{
			Where = predicate;
			return this;
		}

		SqlDelete ISqlGo<SqlDelete>.Go()
		{
			return this;
		}
	}
}