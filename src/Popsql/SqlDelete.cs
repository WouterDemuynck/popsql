using System;
using Popsql.Grammar;
using Popsql.Visitors;

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
		public SqlFrom From
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the predicate determining which rows are deleted by this SQL DELETE statement.
		/// </summary>
		public SqlWhere Where
		{
			get;
			private set;
		}

		/// <summary>
		/// Accepts the specified <paramref name="visitor"/> and dispatches calls to the specific visitor
		/// methods for this object.
		/// </summary>
		/// <param name="visitor">
		/// The <see cref="ISqlVisitor" /> to visit this object with.
		/// </param>
		public override void Accept(ISqlVisitor visitor)
		{
			base.Accept(visitor);

			From?.Accept(visitor);
			Where?.Accept(visitor);
		}
	}
}