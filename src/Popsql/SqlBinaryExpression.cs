using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql
{
    /// <summary>
    /// Represents a binary expression in SQL.
    /// </summary>
    public class SqlBinaryExpression : SqlExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlBinaryExpression"/> class using the specified 
        /// <paramref name="left"/> and <paramref name="right"/> operands and the specified <paramref name="operator"/>.
        /// </summary>
        /// <param name="left">
        /// The left operand of the binary expression.
        /// </param>
        /// <param name="operator">
        /// The operator of the binary expression.
        /// </param>
        /// <param name="right">
        /// The right operand of the binary expression.
        /// </param>
        internal SqlBinaryExpression(SqlExpression left, SqlBinaryOperator @operator, SqlExpression right)
        {
            if (left == null) throw new ArgumentNullException("left");
            if (right == null) throw new ArgumentNullException("right");
            Left = left;
            Operator = @operator;
            Right = right;
        }

        /// <summary>
        /// Gets the left operand of the binary expression.
        /// </summary>
        public SqlExpression Left
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the operator of the binary expression.
        /// </summary>
        public SqlBinaryOperator Operator
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the right operator of the binary expression.
        /// </summary>
        public SqlExpression Right
        {
            get;
            private set;
        }
    }
}
