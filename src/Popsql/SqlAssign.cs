using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql
{
    /// <summary>
    /// Represents an assignment in SQL.
    /// </summary>
    public sealed class SqlAssign
    {
        internal SqlAssign(SqlColumn column, SqlValue value)
        {
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
    }
}
