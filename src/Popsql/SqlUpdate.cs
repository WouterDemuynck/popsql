using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql
{
    /// <summary>
    /// Represents a SQL UPDATE statement.
    /// </summary>
    public class SqlUpdate : SqlStatement
    {
        private List<SqlAssign> _values;

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

        /// <summary>
        /// Gets the values assigned by this <see cref="SqlUpdate"/>.
        /// </summary>
        public IEnumerable<SqlAssign> Values
        {
            get
            {
                return _values ?? (_values = new List<SqlAssign>()); 
            }
        }

        /// <summary>
        /// Sets the value inserted into the specified column.
        /// </summary>
        /// <param name="column">
        /// The column whose value to set.
        /// </param>
        /// <param name="value">
        /// The value to insert into the column.
        /// </param>
        /// <returns>
        /// The current instance of the <see cref="SqlUpdate"/> class.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="column"/> argument is <see langword="null"/> or
        /// an empty array.
        /// </exception>
        /// <remarks>
        /// If you pass <see langword="null"/> for the <paramref name="value"/> argument, it
        /// will be automatically converted to <see cref="SqlConstant.Null"/>.
        /// </remarks>
        public SqlUpdate Set(SqlColumn column, SqlValue value)
        {
            if (column == null) throw new ArgumentNullException("column");
            if (value == null) value = SqlConstant.Null;

            if (_values == null)
            {
                _values = new List<SqlAssign>();
            }
            _values.Add(new SqlAssign(column, value));
            return this;
        }
    }
}
