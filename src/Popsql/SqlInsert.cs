using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql
{
    /// <summary>
    /// Represents a SQL INSERT INTO statement.
    /// </summary>
    public class SqlInsert : SqlStatement
    {
        private List<IEnumerable<SqlValue>> _values;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlInsert"/> class.
        /// </summary>
        public SqlInsert()
        {
        }

        /// <summary>
        /// Returns the expression type of this expression.
        /// </summary>
        public override sealed SqlExpressionType ExpressionType
        {
            get
            {
                return SqlExpressionType.Insert;
            }
        }

        /// <summary>
        /// Gets the table into which this <see cref="SqlInsert" /> inserts rows.
        /// </summary>
        public SqlTable Table
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the collection of columns into which this <see cref="SqlInsert" /> inserts rows.
        /// </summary>
        public IEnumerable<SqlColumn> Columns
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the collection of values to be inserted by this <see cref="SqlInsert"/>.
        /// </summary>
        public IEnumerable<IEnumerable<SqlValue>> Rows
        {
            get
            {
                if (_values == null)
                {
                    _values = new List<IEnumerable<SqlValue>>();
                }
                return _values.ToArray();
            }
        }

        /// <summary>
        /// Sets the table into which rows are inserted.
        /// </summary>
        /// <param name="table">
        /// The table into which rows are inserted.
        /// </param>
        /// <returns>
        /// The current instance of the <see cref="SqlInsert"/> class.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="table"/> argument is <see langword="null"/>.
        /// </exception>
        public SqlInsert Into(SqlTable table)
        {
            return Into(table, null);
        }

        /// <summary>
        /// Sets the table and columns into which rows are inserted.
        /// </summary>
        /// <param name="table">
        /// The table into which rows are inserted.
        /// </param>
        /// <param name="columns">
        /// The columns into which rows are inserted.
        /// </param>
        /// <returns>
        /// The current instance of the <see cref="SqlInsert"/> class.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="table"/> argument is <see langword="null"/>.
        /// </exception>
        public SqlInsert Into(SqlTable table, params SqlColumn[] columns)
        {
            if (table == null) throw new ArgumentNullException("table");

            Table = table;
            Columns = columns;
            return this;
        }

        /// <summary>
        /// Sets the values inserted into the table.
        /// </summary>
        /// <param name="values">
        /// The values to insert into the table.
        /// </param>
        /// <returns>
        /// The current instance of the <see cref="SqlInsert"/> class.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="values"/> argument is <see langword="null"/> or
        /// an empty array.
        /// </exception>
        public SqlInsert Values(params SqlValue[] values)
        {
            if (values == null || !values.Any()) throw new ArgumentNullException("values");
            if (_values == null)
            {
                _values = new List<IEnumerable<SqlValue>>();
            }
            _values.Add(values);
            return this;
        }
    }
}
