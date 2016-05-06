using Popsql.Grammar;

namespace Popsql
{
	/// <summary>
	/// Provides <see langword="static" /> factory methods for creating SQL statement trees.
	/// </summary>
	public static class Sql
	{
		/// <summary>
		/// Creates a <see cref="SqlSelect"/> that selects the specified <paramref name="columns"/> from a table.
		/// </summary>
		/// <param name="columns">
		/// The columns to select from the table.
		/// </param>
		/// <returns>
		/// A <see cref="SqlSelect"/> that selects the specified <paramref name="columns"/> from a table.
		/// </returns>
		public static ISqlSelectClause Select(params SqlColumn[] columns)
		{
			return new SqlSelect.SelectClause(columns);
		}

		/// <summary>
		/// Creates a <see cref="SqlUnion"/> that combines the specified <see cref="SqlSelect"/> <paramref name="statements"/>.
		/// </summary>
		/// <param name="statements"></param>
		/// <returns>
		/// A <see cref="SqlUnion"/> that combines the specified <see cref="SqlSelect"/> <paramref name="statements"/>.
		/// </returns>
		public static SqlUnion Union(params SqlSelect[] statements)
		{
			return new SqlUnion(statements);
		}

		/// <summary>
		/// Creates a <see cref="SqlDelete"/> that deletes rows from a table.
		/// </summary>
		/// <returns>
		/// A <see cref="SqlDelete"/> that deletes rows from a table.
		/// </returns>
		public static ISqlDeleteClause Delete()
		{
			return new SqlDelete.DeleteClause();
		}

		/// <summary>
		/// Creates a <see cref="SqlInsert"/> that inserts rows into a table.
		/// </summary>
		/// <returns>
		/// A <see cref="SqlInsert"/> that inserts rows into a table.
		/// </returns>
		public static ISqlInsertClause Insert()
		{
			return new SqlInsert.InsertClause();
		}

		/// <summary>
		/// Creates a <see cref="SqlUpdate"/> that updates rows in the specified <paramref name="table"/>.
		/// </summary>
		/// <param name="table">
		/// The table to update.
		/// </param>
		/// <returns>
		/// A <see cref="SqlUpdate"/> that updates rows in the specified <paramref name="table"/>.
		/// </returns>
		public static ISqlUpdateClause Update(SqlTable table)
		{
			return new SqlUpdate.UpdateClause(table);
		}
	}
}