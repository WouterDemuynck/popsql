using System;
using System.ComponentModel;
using System.Linq;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL identifier, e.g. a database name, a table name, a column name, ...
	/// </summary>
	public class SqlIdentifier : SqlExpression, IEquatable<SqlIdentifier>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlIdentifier"/> class by parsing the
		/// specified identifier string.
		/// </summary>
		/// <param name="identifier">
		/// The SQL identifier string to parse.
		/// </param>
		public SqlIdentifier(string identifier)
		{
			if (identifier == null) throw new ArgumentNullException(nameof(identifier));
			if (identifier.Length == 0) throw new ArgumentException("The value of the identifier argument must be a non-empty string.", nameof(identifier));

			Segments = SqlIdentifierParser.Parse(identifier);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlIdentifier"/> class using the
		/// specified identifier segments.
		/// </summary>
		/// <param name="segments">
		/// The segments of the SQL identifier to create.
		/// </param>
		public SqlIdentifier(string[] segments)
		{
			if (segments == null) throw new ArgumentNullException(nameof(segments));

			Segments = segments;
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.Identifier;

		/// <summary>
		/// Gets an array containing the path segments that make up this identifier.
		/// </summary>
		public string[] Segments
		{
			get;
		}

		/// <summary>
		/// Implicitly converts a <see cref="string"/> representing a SQL identifier to a <see cref="SqlIdentifier"/> instance.
		/// </summary>
		/// <param name="identifier">
		/// A string representing a SQL identifier.
		/// </param>
		/// <returns>
		/// A <see cref="SqlIdentifier"/> instance representing the specified identifier.
		/// </returns>
		public static implicit operator SqlIdentifier(string identifier)
		{
			return new SqlIdentifier(identifier);
		}

		/// <summary>
		/// Serves as the default hash function. 
		/// </summary>
		/// <returns>
		/// A hash code for the current object.
		/// </returns>
		public override int GetHashCode()
		{
			unchecked
			{
				return Segments.Aggregate(17, (current, segment) => current * 23 + segment.GetHashCode());
			}
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
											  
			return Equals((SqlIdentifier)other);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object.
		/// </summary>
		/// <param name="other">
		/// A <see cref="SqlIdentifier"/> to compare with this object. 
		/// </param>
		/// <returns>
		/// <see langword="true" /> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(SqlIdentifier other)
		{
			if (other == null) return false;
			return Segments.SequenceEqual(other.Segments);
		}
	}
}
