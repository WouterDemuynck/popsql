using Popsql.Text;

namespace Popsql.Dialects
{
	/// <summary>
	/// Provides support for the <b>MySQL</b> SQL dialect.
	/// </summary>
	public class MySqlDialect : SqlDialect
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MySqlDialect"/> class.
		/// </summary>
		public MySqlDialect()
		{
		}

		/// <summary>
		/// Formats the specified identifier name for the <b>MySQL</b> dialect. 
		/// Returns the identifier name enclosed in backtick ('`') characters.
		/// </summary>
		/// <param name="identifier">
		/// The identifier to format.
		/// </param>
		/// <returns>
		/// The formatted SQL identifier.
		/// </returns>
		public override string FormatIdentifier(string identifier)
		{
			return $"`{identifier}`";
		}

		/// <summary>
		/// Formats the specified parameter name for the current SQL dialect.
		/// Returns the parameter name prefixed with an question mark character ('?').
		/// </summary>
		/// <param name="parameterName">
		/// The parameter name to format.
		/// </param>
		/// <returns>
		/// The formatted parameter name.
		/// </returns>
		public override string FormatParameterName(string parameterName)
		{
			return $"?{parameterName}";
		}

		/// <summary>
		/// Writes the specified result set limitation for the current SQL dialect. 
		/// Writes the <c>LIMIT <paramref name="offset"/>, <paramref name="count"/></c> clause
		/// to the output <paramref name="writer"/>.
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
		public override void WriteFetchFirst(SqlWriter writer, int? offset, int? count)
		{
			writer.WriteKeyword(MySqlKeywords.Limit);
			if (offset != null)
			{
				writer.WriteValue(offset.GetValueOrDefault());
				writer.WriteRaw(",");
			}
			writer.WriteValue(count.GetValueOrDefault(int.MaxValue));
		}

		/// <summary>
		/// Provides <see cref="SqlKeyword"/> instances for well-known SQL keywords in the MySQL dialect.
		/// </summary>
		public static class MySqlKeywords
		{
			/// <summary>
			/// Represents the MySQL LIMIT keyword.
			/// </summary>
			public static readonly SqlKeyword Limit = "LIMIT";
		}
	}
}