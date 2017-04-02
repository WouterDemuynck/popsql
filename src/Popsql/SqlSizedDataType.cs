using System;

namespace Popsql
{
	/// <summary>
	/// Reprsents a sized SQL data type.
	/// </summary>
	public class SqlSizedDataType : SqlDataType
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlSizedDataType"/> class using the specified <paramref name="name"/>
		/// and <paramref name="size"/>.
		/// </summary>
		/// <param name="name">
		/// The name of the data type.
		/// </param>
		/// <param name="size">
		/// The size of the data type.
		/// </param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown when the <paramref name="size"/> argument is less than 1 (unless <see langword="null"/> is specified).
		/// </exception>
		public SqlSizedDataType(SqlDataTypeName name, int? size = null)
			: base(name)
		{
			if (size != null && size < 1) throw new ArgumentOutOfRangeException(nameof(size), size, "Size argument must be greater than or equal to 1.");
			Size = size;
		}

		/// <summary>
		/// Gets the size of the data type.
		/// </summary>
		public int? Size
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

			var other = (SqlSizedDataType) obj;
			return base.Equals(other) && Size == other.Size;
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
				return (base.GetHashCode() * 97) ^ Size.GetHashCode();
			}
		}
	}
}