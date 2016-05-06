using System;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL WHERE clause.
	/// </summary>
	public class SqlWhere : SqlClause
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlWhere"/> class.
		/// </summary>
		/// <param name="predicate">
		/// The predicate the SQL WHERE clause uses.
		/// </param>
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
			Predicate.Accept(visitor);
		}
	}
}