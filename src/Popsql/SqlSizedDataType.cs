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
	}
}