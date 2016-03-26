using System;

namespace Popsql
{
	/// <summary>
	/// Represents a sorting expression in SQL.
	/// </summary>
	public class SqlSort : SqlExpression
    {
        internal SqlSort(SqlColumn column, SqlSortOrder sortOrder)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            Column = column;
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Gets the column to be sorted.
        /// </summary>
        public SqlColumn Column
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the order in which rows are sorted for the column.
        /// </summary>
        public SqlSortOrder SortOrder
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the expression type of this expression.
        /// </summary>
        public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Sort;
    }
}
