using System;
using Popsql.Grammar;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL DELETE statement.
	/// </summary>
	public partial class SqlDelete : SqlStatement
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
	}
}