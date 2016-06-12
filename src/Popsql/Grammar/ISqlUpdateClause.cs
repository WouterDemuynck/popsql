using System;

namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL UPDATE clause.
	/// </summary>
	public interface ISqlUpdateClause
	{
		/// <summary>
		/// Sets the value inserted into the specified column.
		/// </summary>
		/// <param name="column">
		/// The column whose value to set.
		/// </param>
		/// <param name="value">
		/// The value to insert into the column.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="column"/> argument is <see langword="null"/> or
		/// an empty array.
		/// </exception>
		/// <remarks>
		/// If you pass <see langword="null"/> for the <paramref name="value"/> argument, it
		/// will be automatically converted to <see cref="SqlConstant.Null"/>.
		/// </remarks>
		ISqlSetClause Set(SqlColumn column, SqlValue value);
	}
}