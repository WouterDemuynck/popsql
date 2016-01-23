using System;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL identifier, e.g. a database name, a table name, a column name, ...
	/// </summary>
	public class SqlIdentifier : SqlExpression
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
			if (identifier == null) throw new ArgumentNullException("identifier");
			if (identifier.Length == 0) throw new ArgumentException("The value of the identifier argument must be a non-empty string.", "identifier");

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
			if (segments == null) throw new ArgumentNullException("segments");

			Segments = segments;
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
		{
			get
			{
				return SqlExpressionType.Identifier;
			}
		}

		/// <summary>
		/// Gets an array containing the path segments that make up this identifier.
		/// </summary>
		public string[] Segments
		{
			get;
			private set;
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
	}
}
