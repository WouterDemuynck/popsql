using System;

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
            Value = value;
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
    }
}
