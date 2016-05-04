using System;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL statement clause.
	/// </summary>
	public abstract class SqlClause : SqlStatement
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlClause"/> class.
		/// </summary>
		protected SqlClause()
		{
		}
	}
}