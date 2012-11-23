using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql
{
    /// <summary>
    /// Represents a constant value in SQL.
    /// </summary>
    public class SqlConstant : SqlValue
    {
        /// <summary>
        /// Represents the NULL SQL constant.
        /// </summary>
        public static readonly SqlConstant Null = new SqlConstant(null);

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlConstant"/> class using
        /// the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// The value of the constant.
        /// </param>
        public SqlConstant(object value)
        {
            Value = value;
        }

        /// <summary>
        /// The value of this <see cref="SqlConstant"/>.
        /// </summary>
        public object Value
        {
            get;
            private set;
        }
    }
}
