using Popsql.Text;

namespace Popsql.Dialects
{
	/// <summary>
	/// Provides the base class for classes providing support for specific SQL dialects.
	/// </summary>
	public class SqlDialect
	{
		private static SqlDialect _current;

		static SqlDialect()
		{
			Default = new SqlDialect();
			Current = Default;
		}

		/// <summary>
		/// Gets a reference to the default SQL dialect.
		/// </summary>
		public static SqlDialect Default
		{
			get;
		}

		/// <summary>
		/// Gets or sets the currently used <see cref="SqlDialect"/>.
		/// </summary>
		public static SqlDialect Current
		{
			get { return _current; }
			set { _current = value ?? Default; }
		}

		/// <summary>
		/// Formats the specified identifier name for the current SQL dialect. The default implementation 
		/// returns the identifier name enclosed in square brackets.
		/// </summary>
		/// <param name="identifier">
		/// The identifier to format.
		/// </param>
		/// <returns>
		/// The formatted SQL identifier.
		/// </returns>
		public virtual string FormatIdentifier(string identifier)
		{
			return $"[{identifier}]";
		}

		/// <summary>
		/// Formats the specified parameter name for the current SQL dialect. The default implementation 
		/// returns the parameter name prefixed with an 'at' character ('@').
		/// </summary>
		/// <param name="parameterName">
		/// The parameter name to format.
		/// </param>
		/// <returns>
		/// The formatted parameter name.
		/// </returns>
		public virtual string FormatParameterName(string parameterName)
		{
			return $"@{parameterName}";
		}

		/// <summary>
		/// Formats the specified string for the current SQL dialect. The default implementation 
		/// returns the table name enclosed in single quotes.
		/// </summary>
		/// <param name="value">
		/// The SQL string to format.
		/// </param>
		/// <returns>
		/// The formatted SQL string.
		/// </returns>
		public virtual string FormatString(string value)
		{
			return $"'{value.Replace("'", "''")}'";
		}

		/// <summary>
		/// Writes the specified result set limitation for the current SQL dialect. The default
		/// implementation uses the <c>OFFSET <paramref name="offset"/> ROWS FETCH FIRST <paramref name="count"/> ROWS ONLY</c>
		/// syntax.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="SqlWriter"/> to write to.
		/// </param>
		/// <param name="offset">
		/// The row offset at which to start.
		/// </param>
		/// <param name="count">
		/// The number of rows to fetch.
		/// </param>
		public virtual void WriteFetchFirst(SqlWriter writer, int? offset, int? count)
		{
			if (offset != null)
			{
				writer.WriteKeyword(SqlKeywords.Offset);
				writer.WriteValue(offset.Value);
				writer.WriteKeyword(SqlKeywords.Rows);
			}

			if (count != null)
			{
				writer.WriteKeyword(SqlKeywords.Fetch);
				writer.WriteKeyword(SqlKeywords.First);
				writer.WriteValue(count.Value);
				writer.WriteKeyword(SqlKeywords.Rows);
				writer.WriteKeyword(SqlKeywords.Only);
			}
		}
	}
}
