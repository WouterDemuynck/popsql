using System;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL data type with precision and scale arguments.
	/// </summary>
	public class SqlScaledDataType : SqlPrecisionDataType
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlScaledDataType"/> class using the specified
		/// <paramref name="precision"/> and <paramref name="scale"/> arguments.
		/// </summary>
		/// <param name="name">
		/// The name of the data type.
		/// </param>
		/// <param name="precision">
		/// The precision of the data type.
		/// </param>
		/// <param name="scale">
		/// The scale of the data type.
		/// </param>
		public SqlScaledDataType(SqlDataTypeName name, int? precision = null, int? scale = null)
			: base(name, precision)
		{
			if (scale != null && precision == null) throw new ArgumentException("The scale argument can only be specified if the precision argument is specified.", nameof(scale));
			if (scale != null && (scale < 0 || scale > precision)) throw new ArgumentOutOfRangeException(nameof(scale), scale, "The scale argument must be greater than or equal to zero and less than or equal to the precision argument.");

			Scale = scale;
		}

		/// <summary>
		/// Gets the scale of the data type.
		/// </summary>
		public int? Scale
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

			var other = (SqlScaledDataType) obj;
			return base.Equals(other) && Scale == other.Scale;
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
				return (base.GetHashCode() * 397) ^ Scale.GetHashCode();
			}
		}
	}
}