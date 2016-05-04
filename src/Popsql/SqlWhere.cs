using System;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL WHERE statement beloning to a specific SQL statement.
	/// </summary>
	public class SqlWhere : SqlClause
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlWhere"/> class
		/// using the specified parent statement.
		/// </summary>
		public SqlWhere(SqlExpression predicate)
		{
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			Predicate = predicate;
		}

		/// <summary>
		/// Gets the predicate this SQL WHERE clause uses.
		/// </summary>
		public SqlExpression Predicate
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Where;

		public override void Accept(ISqlVisitor visitor)
		{
			base.Accept(visitor);
			Predicate?.Accept(visitor);
		}
	}
}