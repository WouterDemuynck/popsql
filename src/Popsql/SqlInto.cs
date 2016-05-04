using System;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL INTO clause of a SQL INSERT statement.
	/// </summary>
	public class SqlInto : SqlClause
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlInto"/> class.
		/// </summary>
		public SqlInto(SqlTableExpression table)
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			Table = table;
		}

		/// <summary>
		/// Gets the <see cref="SqlTableExpression"/> into which rows are inserted.
		/// </summary>
		public SqlTableExpression Table
		{
			get;
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Into;

		public override void Accept(ISqlVisitor visitor)
		{
			base.Accept(visitor);
			Table?.Accept(visitor);
		}
	}
}