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
        /// Returns the expression type of this expression.
        /// </summary>
        public override sealed SqlExpressionType ExpressionType
        {
            get
            {
                return SqlExpressionType.Delete;
            }
        }

        /// <summary>
        /// Gets the table from which to delete rows.
        /// </summary>
        public SqlTable Table
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the predicate determining which rows are deleted by this SQL DELETE statement.
        /// </summary>
        public SqlExpression Predicate
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the <paramref name="table"/> from which to delete rows.
        /// </summary>
        /// <param name="table">
        /// The <see cref="SqlTable"/> from which to delete rows.
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

        /// <summary>
        /// Sets the predicate used for determining which rows are deleted by this SQL DELETE statement.
        /// </summary>
        /// <param name="predicate">
        /// The predicate used for determining which rows are deleted by this SQL DELETE statement.
        /// </param>
        /// <returns>
        /// The current instance of the <see cref="SqlDelete"/> class.
        /// </returns>
        public SqlDelete Where(SqlExpression predicate)
        {
            Predicate = predicate;
            return this;
        }
    }
}
