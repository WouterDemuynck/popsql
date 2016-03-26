using System;

namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the grammar for the SQL DELETE clause.
	/// </summary>
	public interface ISqlDeleteClause
	{
		/// <summary>
		/// Sets the <paramref name="table"/> from which to delete rows.
		/// </summary>
		/// <param name="table">
		/// The <see cref="SqlTable"/> from which to delete rows.
		/// </param>
		/// <returns>
		/// The next grammatical possibilities in the SQL statement.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="table"/> argument is <see langword="null"/>.
		/// </exception>
		ISqlDeleteFromClause From(SqlTable table);
	}
}