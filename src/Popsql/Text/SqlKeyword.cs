using System;

namespace Popsql.Text
{
	/// <summary>
	/// Represents a SQL keyword.
	/// </summary>
	public class SqlKeyword
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlKeyword"/> class.
		/// </summary>
		/// <param name="keyword"></param>
		public SqlKeyword(string keyword)
		{
			if (string.IsNullOrWhiteSpace(keyword)) throw new ArgumentNullException(nameof(keyword));

			Keyword = keyword;
		}

		/// <summary>
		/// Gets the <see cref="string"/> representation of the <see cref="SqlKeyword"/>.
		/// </summary>
		public string Keyword
		{
			get;
			private set;
		}

		/// <summary>
		/// Implicitly converts a <see cref="string"/> representing a SQL keyword to a <see cref="SqlKeyword"/> instance.
		/// </summary>
		/// <param name="keyword">
		/// The SQL keyword.
		/// </param>
		/// <returns>
		/// A <see cref="SqlKeyword"/> instance representing the specified SQL keyword.
		/// </returns>
		public static implicit operator SqlKeyword(string keyword)
		{
			return new SqlKeyword(keyword);
		}
	}
}
