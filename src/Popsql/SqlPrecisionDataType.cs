using System;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL data type with a precision argument.
	/// </summary>
	public class SqlPrecisionDataType : SqlDataType
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlPrecisionDataType"/> class using the specified <paramref name="name"/>
		/// and <paramref name="precision"/>.
		/// </summary>
		/// <param name="name">
		/// The name of the data type.
		/// </param>
		/// <param name="precision">
		/// The precision of the data type.
		/// </param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown when the <paramref name="precision"/> argument is less than 1 (unless <see langword="null"/> is specified).
		/// </exception>
		public SqlPrecisionDataType(SqlDataTypeName name, int? precision = null)
			: base(name)
		{
			if (precision != null && precision < 1) throw new ArgumentOutOfRangeException(nameof(precision), precision, "Precision argument must be greater than or equal to 1.");
			Precision = precision;
		}

		/// <summary>
		/// Gets the precision of the data type.
		/// </summary>
		public int? Precision
		{
			get;
		}

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the specified object  is equal to the current object; otherwise, <see langword="false"/>.
		/// </returns>
		/// <param name="obj">
		/// The object to compare with the current object.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;

			var other = (SqlPrecisionDataType) obj;
			return base.Equals(other) && Precision == other.Precision;
		}

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>
		/// A hash code for the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 197) ^ Precision.GetHashCode();
			}
		}
	}
}