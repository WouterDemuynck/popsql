using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql
{
    /// <summary>
    /// Provides <see langword="static" /> factory methods for creating SQL statement trees.
    /// </summary>
    public static class Sql
    {
        /// <summary>
        /// Creates a <see cref="SqlSelect"/> that selects the specified <paramref name="columns"/> from a table.
        /// </summary>
        /// <param name="columns">
        /// The columns to select from the table.
        /// </param>
        /// <returns>
        /// A <see cref="SqlSelect"/> that selects the specified <paramref name="columns"/> from a table.
        /// </returns>
        public static SqlSelect Select(params SqlColumn[] columns)
        {
            return new SqlSelect(columns);
        }

        /// <summary>
        /// Creates a <see cref="SqlUpdate"/> that updates data in the specified <paramref name="table"/>.
        /// </summary>
        /// <param name="table">
        /// The table to update.
        /// </param>
        /// <returns>
        /// A <see cref="SqlUpdate"/> that updates data in the specified <paramref name="table"/>.
        /// </returns>
        public static SqlUpdate Update(SqlTable table)
        {
            return new SqlUpdate(table);
        }

        /// <summary>
        /// Creates a <see cref="SqlInsert"/> that inserts data into a table.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlInsert"/> that inserts data into a table.
        /// </returns>
        public static SqlInsert Insert()
        {
            return new SqlInsert();
        }

        /// <summary>
        /// Creates a <see cref="SqlDelete"/> that deletes data from a table.
        /// </summary>
        /// <returns>
        /// A <see cref="SqlDelete"/> that deletes data from a table.
        /// </returns>
        public static SqlDelete Delete()
        {
            return new SqlDelete();
        }
    }
}
