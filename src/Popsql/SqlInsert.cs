using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql
{
    /// <summary>
    /// Represents a SQL INSERT INTO statement.
    /// </summary>
    public class SqlInsert
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlInsert"/> class.
        /// </summary>
        public SqlInsert()
        {
        }

        /// <summary>
        /// Gets the table into which this <see cref="SqlInsert" /> inserts data.
        /// </summary>
        public SqlTable Target
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the table into which data is inserted.
        /// </summary>
        /// <param name="table">
        /// The table into which data is inserted.
        /// </param>
        /// <returns>
        /// The current instance of the <see cref="SqlInsert"/> class.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="table"/> argument is <see langword="null"/>.
        /// </exception>
        public SqlInsert Into(SqlTable table)
        {
            if (table == null) throw new ArgumentNullException("table");

            Target = table;
            return this;
        }
    }
}
