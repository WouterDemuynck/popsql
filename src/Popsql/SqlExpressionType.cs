namespace Popsql
{
	/// <summary>
	/// Describes the expression type for the nodes of a SQL expression tree.
	/// </summary>
	public enum SqlExpressionType
	{
		/// <summary>
		/// Represents a SQL SELECT statement.
		/// </summary>
		Select,
		/// <summary>
		/// Represents a SQL identifier.
		/// </summary>
		Identifier,
		/// <summary> 
		/// Represents a table in SQL. 
		/// </summary> 
		Table
	}
}