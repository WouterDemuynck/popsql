using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql
{
    /// <summary>
    /// Represents a column in a SQL statement.
    /// </summary>
    public class SqlColumn : SqlExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlColumn"/> class using the
        /// specified column name.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="columnName"/> argument is <see langword="null"/>,
        /// an empty string, or a string containing only white-space characters.
        /// </exception>
        public SqlColumn(string columnName)
            : this(null, columnName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlColumn"/> class using the
        /// specified column name and alias.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <param name="alias">
        /// The alias to use for the column.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="columnName"/> argument is <see langword="null"/>,
        /// an empty string, or a string containing only white-space characters.
        /// </exception>
        public SqlColumn(string columnName, string alias)
            : this(null, columnName, alias)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlColumn"/> class using the
        /// specified table name, column name and alias.
        /// </summary>
        /// <param name="tableName">
        /// The name of the table to which the column belongs.
        /// </param>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <param name="alias">
        /// The alias to use for the column.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="columnName"/> argument is <see langword="null"/>,
        /// an empty string, or a string containing only white-space characters.
        /// </exception>
        public SqlColumn(string tableName, string columnName, string alias)
        {
            if (string.IsNullOrWhiteSpace(columnName)) throw new ArgumentNullException("columnName");

            TableName = tableName;
            ColumnName = columnName;
            Alias = alias;
        }

        /// <summary>
        /// Gets the name of the table to which the column belongs.
        /// </summary>
        public string TableName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        public string ColumnName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the alias used for the column
        /// </summary>
        public string Alias
        {
            get;
            private set;
        }

        /// <summary>
        /// Implicitly converts a <see cref="String"/> representing a column name to a <see cref="SqlColumn"/> instance.
        /// </summary>
        /// <param name="columnName">
        /// The name of the table.
        /// </param>
        /// <returns>
        /// A <see cref="SqlColumn"/> instance representing the specified column.
        /// </returns>
        public static implicit operator SqlColumn(string columnName)
        {
            return new SqlColumn(columnName);
        }
    }
}
