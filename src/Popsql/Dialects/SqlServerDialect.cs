using Popsql.Text;

namespace Popsql.Dialects
{
	/// <summary>
	/// Provides support for the <b>SQL Server (T-SQL)</b> SQL dialect.
	/// </summary>
	public class SqlServerDialect : SqlDialect
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MySqlDialect"/> class.
		/// </summary>
		public SqlServerDialect()
		{
		}

		/// <summary>
		/// Writes the specified result set limitation for the current SQL dialect. 
		/// Writes the <c>OFFSET <paramref name="offset"/> ROWS FETCH FIRST <paramref name="count"/> ROWS ONLY</c> clause
		/// to the output <paramref name="writer"/>.
		/// syntax.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="SqlWriter"/> to write to.
		/// </param>
		/// <param name="offset">
		/// The row offset at which to start.
		/// </param>
		/// <param name="count">
		/// The number of rows to fetch.
		/// </param>
		public override void WriteFetchFirst(SqlWriter writer, int? offset, int? count)
		{
			// SQL Server requires an offset...
			if (offset == null && count != null) offset = 0;

			base.WriteFetchFirst(writer, offset, count);
		}
	}
}