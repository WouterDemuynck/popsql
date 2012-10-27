using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql
{
    /// <summary>
    /// Represents a table in a SQL statement.
    /// </summary>
    public class SqlTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTable"/> class using the
        /// specified table name.
        /// </summary>
        /// <param name="tableName">
        /// The name of the table.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="tableName"/> argument is <see langword="null"/>,
        /// an empty string, or a string containing only white-space characters.
        /// </exception>
        public SqlTable(string tableName)
            : this(tableName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTable"/> class using the
        /// specified table name and alias.
        /// </summary>
        /// <param name="tableName">
        /// The name of the table.
        /// </param>
        /// <param name="alias">
        /// The alias to use for the table.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="tableName"/> argument is <see langword="null"/>,
        /// an empty string, or a string containing only white-space characters.
        /// </exception>
        public SqlTable(string tableName, string alias)
        {
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentNullException("tableName");

            TableName = tableName;
            Alias = alias;
        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        public string TableName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the alias used for the table.
        /// </summary>
        public string Alias
        {
            get;
            private set;
        }

        /// <summary>
        /// Implicitly converts a <see cref="String"/> representing a table name to a <see cref="SqlTable"/> instance.
        /// </summary>
        /// <param name="tableName">
        /// The name of the table.
        /// </param>
        /// <returns>
        /// A <see cref="SqlTable"/> instance representing the specified table.
        /// </returns>
        public static implicit operator SqlTable(string tableName)
        {
            return new SqlTable(tableName);
        }
    }
}
