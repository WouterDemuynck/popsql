using System;

namespace Popsql
{
	/// <summary>
	/// Represents the name of a SQL data type.
	/// </summary>
	public class SqlDataTypeName
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlDataTypeName"/> class using the specified <paramref name="name"/>.
		/// </summary>
		/// <param name="name">
		/// The name of the SQL data type.
		/// </param>
		public SqlDataTypeName(string name)
		{
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

			Name = name;
		}

		/// <summary>
		/// Gets the <see cref="string"/> representation of the <see cref="SqlDataTypeName"/>.
		/// </summary>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Implicitly converts a <see cref="string"/> representing a SQL data type name to a <see cref="SqlDataTypeName"/> instance.
		/// </summary>
		/// <param name="name">
		/// The SQL name.
		/// </param>
		/// <returns>
		/// A <see cref="SqlDataTypeName"/> instance representing the specified SQL data type name.
		/// </returns>
		public static implicit operator SqlDataTypeName(string name)
		{
			return new SqlDataTypeName(name);
		}
	}
}