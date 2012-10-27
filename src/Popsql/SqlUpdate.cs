using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql
{
    /// <summary>
    /// Represents a SQL UPDATE statement.
    /// </summary>
    public class SqlUpdate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlUpdate"/> class using
        /// the specified <paramref name="table"/>.
        /// </summary>
        /// <param name="table">
        /// The table updated by the new <see cref="SqlUpdate"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="table"/> argument is <see langword="null"/>.
        /// </exception>
        public SqlUpdate(SqlTable table)
        {
            if (table == null) throw new ArgumentNullException("table");
            Target = table;
        }

        /// <summary>
        /// Gets the table updated by this <see cref="SqlUpdate" />.
        /// </summary>
        public SqlTable Target
        {
            get;
            private set;
        }
    }
}
