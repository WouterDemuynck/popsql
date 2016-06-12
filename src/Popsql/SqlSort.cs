using System;
using Popsql.Visitors;

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
			SortOrder.Accept(visitor);
		}
    }
}
