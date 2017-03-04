namespace Popsql
{
	/// <summary>
	/// Provides <see cref="SqlDataTypeName"/> instances for well-known SQL data types.
	/// </summary>
	public static class SqlDataTypeNames
	{
		/// <summary>
		/// Represents the SQL TINYINT data type.
		/// </summary>
		public static readonly SqlDataTypeName Bit = "BIT";

		/// <summary>
		/// Represents the SQL TINYINT data type.
		/// </summary>
		public static readonly SqlDataTypeName TinyInt = "TINYINT";

		/// <summary>
		/// Represents the SQL SMALLINT data type.
		/// </summary>
		public static readonly SqlDataTypeName SmallInt = "SMALLINT";

		/// <summary>
		/// Represents the SQL INT data type.
		/// </summary>
		public static readonly SqlDataTypeName Int = "INT";

		/// <summary>
		/// Represents the SQL BIGINT data type.
		/// </summary>
		public static readonly SqlDataTypeName BigInt = "BIGINT";

		/// <summary>
		/// Represents the SQL CHAR data type.
		/// </summary>
		public static readonly SqlDataTypeName Char = "CHAR";

		/// <summary>
		/// Represents the SQL VARCHAR data type.
		/// </summary>
		public static readonly SqlDataTypeName VarChar = "VARCHAR";

		/// <summary>
		/// Represents the SQL FLOAT data type.
		/// </summary>
		public static readonly SqlDataTypeName Float = "FLOAT";

		/// <summary>
		/// Represents the SQL DECIMAL data type.
		/// </summary>
		public static readonly SqlDataTypeName Decimal = "DECIMAL";

		/// <summary>
		/// Represents the SQL BINARY data type.
		/// </summary>
		public static readonly SqlDataTypeName Binary = "BINARY";

		/// <summary>
		/// Represents the SQL BINARY data type.
		/// </summary>
		public static readonly SqlDataTypeName VarBinary = "VARBINARY";
	}
}