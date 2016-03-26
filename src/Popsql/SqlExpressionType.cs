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
		/// Represents a SQL DELETE statement.
		/// </summary>
		Delete,
		/// <summary>
		/// Represents a SQL INSERT statement.
		/// </summary>
		Insert,
		/// <summary>
		/// Represents a SQL UPDATE statement.
		/// </summary>
		Update,
		/// <summary>
		/// Represents a SQL identifier.
		/// </summary>
		Identifier,
		/// <summary> 
		/// Represents a table in SQL. 
		/// </summary> 
		Column,
		/// <summary>
		/// Represents a table in SQL.
		/// </summary>
		Table,
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
		/// Represents a sorting expression in SQL.
		/// </summary>
		Sort,
		/// <summary>
		/// Represents a joining expression expression in SQL.
		/// </summary>
		Join,
		/// <summary>
		/// Represents a function call in SQL.
		/// </summary>
		Function,
		/// <summary>
		/// Represents a SQL FROM clause.
		/// </summary>
		From,
		/// <summary>
		/// Represents an assignment expression in SQL.
		/// </summary>
		Assign,
		/// <summary>
		/// Represents a SQL WHERE clause.
		/// </summary>
		Where,
		/// <summary>
		/// Represents a SQL ORDER BY clause.
		/// </summary>
		OrderBy,
		/// <summary>
		/// Represents a UNION operator in SQL.
		/// </summary>
		Union
	}
}