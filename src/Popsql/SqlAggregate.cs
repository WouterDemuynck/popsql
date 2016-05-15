namespace Popsql
{
	/// <summary>
	/// Provides static factory methods for accessing standard SQL aggregate functions.
	/// </summary>
	public static class SqlAggregate
	{
		/// <summary>
		/// Returns a new <see cref="SqlFunction"/> representing the SQL COUNT aggregate function for 
		/// the specified <paramref name="column"/>.
		/// </summary>
		/// <param name="column">
		/// The column for which the count of items will be determined.
		/// </param>
		/// <param name="alias">
		/// The alias to use for the function.
		/// </param>
		/// <returns>
		/// A new <see cref="SqlFunction"/> instance.
		/// </returns>
		public static SqlFunction Count(SqlColumn column, string alias = null)
		{
			return new SqlFunction("COUNT", new[] { column }, alias);
		}
		/// <summary>
		/// Returns a new <see cref="SqlFunction"/> representing the SQL SUM aggregate function for 
		/// the specified <paramref name="column"/>.
		/// </summary>
		/// <param name="column">
		/// The column for which the sum of the items will be determined.
		/// </param>
		/// <param name="alias">
		/// The alias to use for the function.
		/// </param>
		/// <returns>
		/// A new <see cref="SqlFunction"/> instance.
		/// </returns>
		public static SqlFunction Sum(SqlColumn column, string alias = null)
		{
			return new SqlFunction("SUM", new[] { column }, alias);
		}

		/// <summary>
		/// Returns a new <see cref="SqlFunction"/> representing the SQL AVG aggregate function for 
		/// the specified <paramref name="column"/>.
		/// </summary>
		/// <param name="column">
		/// The column for which the average of the items will be determined.
		/// </param>
		/// <param name="alias">
		/// The alias to use for the function.
		/// </param>
		/// <returns>
		/// A new <see cref="SqlFunction"/> instance.
		/// </returns>
		public static SqlFunction Average(SqlColumn column, string alias = null)
		{
			return new SqlFunction("AVG", new[] { column }, alias);
		}

		/// <summary>
		/// Returns a new <see cref="SqlFunction"/> representing the SQL MAX aggregate function for 
		/// the specified <paramref name="column"/>.
		/// </summary>
		/// <param name="column">
		/// The column for which the maximum of the items will be determined.
		/// </param>
		/// <param name="alias">
		/// The alias to use for the function.
		/// </param>
		/// <returns>
		/// A new <see cref="SqlFunction"/> instance.
		/// </returns>
		public static SqlFunction Max(SqlColumn column, string alias = null)
		{
			return new SqlFunction("MAX", new[] { column }, alias);
		}

		/// <summary>
		/// Returns a new <see cref="SqlFunction"/> representing the SQL MIN aggregate function for 
		/// the specified <paramref name="column"/>.
		/// </summary>
		/// <param name="column">
		/// The column for which the minimum of the items will be determined.
		/// </param>
		/// <param name="alias">
		/// The alias to use for the function.
		/// </param>
		/// <returns>
		/// A new <see cref="SqlFunction"/> instance.
		/// </returns>
		public static SqlFunction Min(SqlColumn column, string alias = null)
		{
			return new SqlFunction("MIN", new[] { column }, alias);
		}
	}
}