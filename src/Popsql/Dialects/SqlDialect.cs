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
	}
}
