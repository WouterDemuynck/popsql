using System;

namespace Popsql
{
	/// <summary>
	/// Provides static factory methods for accessing standard SQL functions.
	/// </summary>
	public static class SqlFunctions
	{
		/// <summary>
		/// Returns a new <see cref="SqlFunction"/> representing the SQL COUNT aggregate function for 
		/// the specified <paramref name="column"/>.
		/// </summary>
		/// <param name="column">
		/// The column for which the count of items will be determined.
		/// </param>
		/// <returns>
		/// A new <see cref="SqlFunction"/> instance.
		/// </returns>
		public static SqlFunction Count(SqlColumn column)
		{
			return new SqlFunction("COUNT", new[] { column });
		}
	}
}