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
			return "`" + identifier + "`";
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
			return "?" + parameterName;
		}
	}
}