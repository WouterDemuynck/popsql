namespace Popsql.Text
{
	/// <summary>
	/// Provides the base class for classes providing support for specific SQL dialects.
	/// </summary>
	public class SqlDialect
	{
		/// <summary>
		/// Represents the default SQL dialect.
		/// </summary>
		public static readonly SqlDialect Default = new SqlDialect();

		/// <summary>
		/// Formats the specified table name for the current SQL dialect. The default implementation 
		/// returns the table name enclosed in square brackets.
		/// </summary>
		/// <param name="tableName">
		/// The table name to format.
		/// </param>
		/// <returns>
		/// The formatted table name.
		/// </returns>
		public virtual string FormatTableName(string tableName)
		{
			return "[" + tableName + "]";
		}

		/// <summary>
		/// Formats the specified parameter name for the current SQL dialect. The default implementation 
		/// returns the parameter name prefixed with an 'at' sign ('@').
		/// </summary>
		/// <param name="parameterName">
		/// The parameter name to format.
		/// </param>
		/// <returns>
		/// The formatted parameter name.
		/// </returns>
		public virtual string FormatParameterName(string parameterName)
		{
			return "@" + parameterName;
		}

		/// <summary>
		/// Formats the specified column name for the current SQL dialect. The default implementation
		/// returns the column name enclosed in square brackets.
		/// </summary>
		/// <param name="columnName">
		/// The column name to format.
		/// </param>
		/// <returns>
		/// The formatted column name.
		/// </returns>
		public virtual string FormatColumnName(string columnName)
		{
			return "[" + columnName + "]";
		}
	}
}
