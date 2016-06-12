using System;
using Popsql.Visitors;

namespace Popsql
{
    /// <summary>
    /// Represents an assignment in SQL.
    /// </summary>
    public sealed class SqlAssign : SqlExpression
    {
        internal SqlAssign(SqlColumn column, SqlValue value)
        {
			if (column == null) throw new ArgumentNullException(nameof(column));
            Column = column;
            Value = value ?? SqlConstant.Null;
        }

        /// <summary>
        /// Gets the column to which a value is assigned.
        /// </summary>
        public SqlColumn Column
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value assigned to the column.
        /// </summary>
        public SqlValue Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the expression type of this expression.
        /// </summary>
        public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Assign;

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
			Column.Accept(visitor);
			visitor.Visit(SqlBinaryOperator.Equal);
			Value.Accept(visitor);
	    }
    }
}
