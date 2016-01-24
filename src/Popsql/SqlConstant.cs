using System;

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
        /// Returns the expression type of this expression.
        /// </summary>
        public override sealed SqlExpressionType ExpressionType
        {
            get
            {
                return SqlExpressionType.Constant;
            }
        }

        /// <summary>
        /// The value of this <see cref="SqlConstant"/>.
        /// </summary>
        public object Value
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Converts the specified <see cref="SqlConstant"/> to a <see cref="SqlParameter"/> when
        /// concatenated with a <see cref="String"/>.
        /// </summary>
        /// <param name="parameterName">
        /// The name of the parameter.
        /// </param>
        /// <param name="value">
        /// The value of the parameter.
        /// </param>
        /// <returns>
        /// A <see cref="SqlParameter"/> representing the <paramref name="value"/> as a SQL parameter
        /// with the specified name.
        /// </returns>
        public static SqlParameter operator +(string parameterName, SqlConstant value)
        {
            if (value == null) throw new ArgumentNullException("value");
            return new SqlParameter(parameterName, value.Value);
        }
    }
}
