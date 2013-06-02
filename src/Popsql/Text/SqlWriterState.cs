using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql.Text
{
    /// <summary>
    /// Specifies the state of the <see cref="SqlWriter"/>.
    /// </summary>
    public enum SqlWriterState
    {
        /// <summary>
        /// Indicates that no writing has been done as of yet.
        /// </summary>
        Start,
        /// <summary>
        /// Indicates that the start of a SQL SELECT statement has been written.
        /// </summary>
        StartSelect,
        /// <summary>
        /// Indicates that the writer is currently writing a SQL SELECT statement.
        /// </summary>
        Select,
        /// <summary>
        /// Indicates that the start of a SQL FROM clause has been written.
        /// </summary>
        StartFrom,
        /// <summary>
        /// Indicates that the writer is currently writing a SQL FROM clause.
        /// </summary>
        From,
        /// <summary>
        /// Indicates that the start of a SQL WHERE clause has been written.
        /// </summary>
        StartWhere,
        /// <summary>
        /// Indicates that the writer is currently writing a SQL WHERE clause.
        /// </summary>
        Where,
        /// <summary>
        /// Indicates that the start of a SQL expression has been written.
        /// </summary>
        StartExpression,
        /// <summary>
        /// Indicates that the writer is currently writing a SQL expression.
        /// </summary>
        Expression,
        /// <summary>
        /// Indicates that the start of a SQL UPDATE statement has been written.
        /// </summary>
        StartUpdate,
        /// <summary>
        /// Indicates that the writer is currently writing a SQL UPDATE statement
        /// </summary>
        Update,
        /// <summary>
        /// Indicates that the start of a SQL SET clause of an UPDATE statement has been written.
        /// </summary>
        StartSet,
        /// <summary>
        /// Indicates that the writer is currently writing a SQL SET clause of an UPDATE statement.
        /// </summary>
        Set,
        /// <summary>
        /// Indicates that the start of a SQL INSERT statement has been written.
        /// </summary>
        StartInsert,
        /// <summary>
        /// Indicates that the writer is currently writing a SQL INTO clause.
        /// </summary>
        StartInto,
        /// <summary>
        /// Indicates that the writer is currently writing a SQL INTO clause.
        /// </summary>
        Into,
        /// <summary>
        /// Indicates that the writer is currently writing a SQL VALUES clause of an INSERT statement.
        /// </summary>
        StartValues,
        /// <summary>
        /// Indicates that the writer is currently writing a SQL VALUES clause of an INSERT statement.
        /// </summary>
        Values,
        /// <summary>
        /// Indicates that the writer is currently finished with a SQL VALUES clause of an INSERT statement.
        /// </summary>
        EndValues,
        /// <summary>
        /// Indicates that the start of a SQL DELETE statement has been written.
        /// </summary>
        StartDelete,
        /// <summary>
        /// Indicates that the end of a statement has been reached.
        /// </summary>
        End,
        /// <summary>
        /// Indicates that the writer has been closed.
        /// </summary>
        Closed
    }
}
