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
		Column,
		/// <summary>
		/// Represents a constant value in SQL.
		/// </summary>
		Constant,
		/// <summary>
		/// Represents a named parameter in SQL.
		/// </summary>
		Parameter,
		/// <summary>
		/// Represents a binary expression in SQL.
		/// </summary>
		Binary,
		/// <summary>
		/// Represents a joining expression expression in SQL.
		/// </summary>
		Join
	}
}