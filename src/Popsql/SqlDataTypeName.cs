using System;

namespace Popsql
{
	/// <summary>
	/// Represents the name of a SQL data type.
	/// </summary>
	public class SqlDataTypeName : IEquatable<SqlDataTypeName>
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

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false"/>.
		/// </returns>
		/// <param name="other">
		/// An object to compare with this object.
		/// </param>
		public bool Equals(SqlDataTypeName other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name);
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
			return Equals((SqlDataTypeName) obj);
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
			return Name.GetHashCode();
		}
	}
}