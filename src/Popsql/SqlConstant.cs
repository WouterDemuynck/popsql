using System;

namespace Popsql
{
	/// <summary>
	/// Represents a constant value in SQL.
	/// </summary>
	public class SqlConstant : SqlValue, IEquatable<SqlConstant>
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
			=> SqlExpressionType.Constant;

		/// <summary>
        /// The value of this <see cref="SqlConstant"/>.
        /// </summary>
        public object Value
        {
            get;
		}

		/// <summary>
		/// Serves as the default hash function. 
		/// </summary>
		/// <returns>
		/// A hash code for the current object.
		/// </returns>
		public override int GetHashCode()
		{
			return Value?.GetHashCode() ?? -1;
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object.
		/// </summary>
		/// <param name="other">
		/// An object to compare with this object. 
		/// </param>
		/// <returns>
		/// <see langword="true" /> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <see langword="false" />.
		/// </returns>
		public override bool Equals(object other)
		{
			if (other == null || GetType() != other.GetType())
				return false;

			return Equals((SqlConstant) other);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object.
		/// </summary>
		/// <param name="other">
		/// A <see cref="SqlConstant"/> to compare with this object. 
		/// </param>
		/// <returns>
		/// <see langword="true" /> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(SqlConstant other)
		{
			if (other == null) return false;
			return Value?.Equals(other.Value) ?? other.Value == null;
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
            if (value == null) throw new ArgumentNullException(nameof(value));
            return new SqlParameter(parameterName, value.Value);
        }
	}
}
