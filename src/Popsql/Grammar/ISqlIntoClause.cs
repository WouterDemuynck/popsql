using System;

namespace Popsql.Grammar
{
	/// <summary>
	/// Provides grammar for the SQL INSERT INTO clause.
	/// </summary>
	public interface ISqlIntoClause
	{
		/// <summary>
		/// Sets the values inserted into the table.
		/// </summary>
		/// <param name="values">
		/// The values to insert into the table.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="values"/> argument is <see langword="null"/> or
		/// an empty array.
		/// </exception>
		ISqlValuesClause Values(params SqlValue[] values);
	}
}