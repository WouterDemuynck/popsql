using System;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL FROM statement beloning to a specific SQL statement.
	/// </summary>
	public class SqlFrom : SqlClause
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlFrom"/> class.
		/// </summary>
		public SqlFrom(SqlTableExpression table)
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			Table = table;
		}

		/// <summary>
		/// Gets the <see cref="SqlTableExpression"/> from which rows are selected.
		/// </summary>
		public SqlTableExpression Table
		{
			get;
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.From;

		public override void Accept(ISqlVisitor visitor)
		{
			base.Accept(visitor);
			Table.Accept(visitor);
		}
	}
}