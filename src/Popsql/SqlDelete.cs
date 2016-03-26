using System;
using Popsql.Grammar;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL DELETE statement.
	/// </summary>
	public class SqlDelete : SqlStatement, ISqlDeleteClause, ISqlDeleteFromClause, ISqlWhereClause<SqlDelete>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlDelete"/> class.
		/// </summary>
		public SqlDelete()
		{
		}

		/// <summary>
		/// Returns the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Delete;

		/// <summary>
		/// Gets the table from which to delete rows.
		/// </summary>
		public SqlTable From
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the predicate determining which rows are deleted by this SQL DELETE statement.
		/// </summary>
		public SqlExpression Where
		{
			get;
			private set;
		}

		ISqlDeleteFromClause ISqlDeleteClause.From(SqlTable table)
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			From = table;
			return this;
		}

		ISqlWhereClause<SqlDelete> ISqlDeleteFromClause.Where(SqlExpression predicate)
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