using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql
{
    /// <summary>
    /// Represents a SQL SELECT statement.
    /// </summary>
    public class SqlSelect : SqlStatement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlSelect"/> class using the
        /// specified <paramref name="columns"/>.
        /// </summary>
        /// <param name="columns">
        /// The columns selected by the new <see cref="SqlSelect"/> instance.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="columns"/> argument is <see langword="null"/>.
        /// </exception>
        public SqlSelect(IEnumerable<SqlColumn> columns)
        {
            if (columns == null) throw new ArgumentNullException("columns");
            Columns = columns;
        }

        /// <summary>
        /// Gets the table from which data is selected by this SQL SELECT statement.
        /// </summary>
        public SqlTable Table
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the collection of columns selected by this SQL SELECT statement.
        /// </summary>
        public IEnumerable<SqlColumn> Columns
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the table from which data is selected by this SQL SELECT statement.
        /// </summary>
        /// <param name="table">
        /// The table from which data is selected.
        /// </param>
        /// <returns>
        /// The current instance of the <see cref="SqlSelect"/> class.
        /// </returns>
        public SqlSelect From(SqlTable table)
        {
            Table = table;
            return this;
        }
    }
}
