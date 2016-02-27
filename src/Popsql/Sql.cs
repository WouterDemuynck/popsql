using Popsql.Grammar;

namespace Popsql
{
	public static class Sql
	{
		public static ISqlSelectClause Select(params SqlColumn[] columns)
		{
			return new SqlSelect(columns);
		}

		public static ISqlDeleteClause Delete()
		{
			return new SqlDelete();
		}

		public static ISqlInsertClause Insert()
		{
			return new SqlInsert();
		}

		public static ISqlUpdateClause Update(SqlTable table)
		{
			return new SqlUpdate(table);
		}
	}
}