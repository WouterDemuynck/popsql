using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql
{
    /// <summary>
    /// Represents a sorting expression in SQL.
    /// </summary>
    public class SqlSort
    {
        internal SqlSort(SqlColumn column, SqlSortOrder sortOrder)
        {
            if (column == null) throw new ArgumentNullException("column");
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
    }
}
