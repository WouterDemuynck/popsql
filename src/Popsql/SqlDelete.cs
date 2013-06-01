using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql
{
    /// <summary>
    /// Represents a SQL DELETE statement.
    /// </summary>
    public class SqlDelete : SqlStatement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDelete"/> class.
        /// </summary>
        public SqlDelete()
        {
        }

        /// <summary>
        /// Gets the table from which to delete data.
        /// </summary>
        public SqlTable Table
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the <paramref name="table"/> from which to delete data.
        /// </summary>
        /// <param name="table">
        /// The <see cref="SqlTable"/> from which to delete data.
        /// </param>
        /// <returns>
        /// The current instance of the <see cref="SqlDelete"/> class.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="table"/> argument is <see langword="null"/>.
        /// </exception>
        public SqlDelete From(SqlTable table)
        {
            if (table == null) throw new ArgumentNullException("table");

            Table = table;
            return this;
        }
    }
}
