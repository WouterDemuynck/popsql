using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql
{
    /// <summary>
    /// Represents all available binary operators in SQL.
    /// </summary>
    public enum SqlBinaryOperator
    {
        /// <summary>
        /// Represents the logical <b>AND</b> operator.
        /// </summary>
        And,
        /// <summary>
        /// Represents the logical <b>OR</b> operator.
        /// </summary>
        Or,
        /// <summary>
        /// Represents the equality comparison operator (<c>=</c>).
        /// </summary>
        Equal,
        /// <summary>
        /// Represents the inequality comparison operator (<c>&lt;&gt;</c>).
        /// </summary>
        NotEqual,
        /// <summary>
        /// Represents the "greater than" comparison operator (<c>&gt;</c>).
        /// </summary>
        GreaterThan,
        /// <summary>
        /// Represents the "greater than or equal to" comparison operator (<c>&gt;=</c>).
        /// </summary>
        GreaterThanOrEqual,
        /// <summary>
        /// Represents the "less than" comparison operator (<c>&lt;</c>).
        /// </summary>
        LessThan,
        /// <summary>
        /// Represents the "less than or equal to" comparison operator (<c>&lt;</c>).
        /// </summary>
        LessThanOrEqual,
        /// <summary>
        /// Represents the "like" comparison operator (<c>LIKE</c>).
        /// </summary>
        Like,
		/// <summary>
		/// Represents the "in" comparison operator (<c>IN</c>).
		/// </summary>
	    In
    }
}
