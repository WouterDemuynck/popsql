using System;
using System.Linq;

namespace Popsql
{
	/// <summary> 
	/// Represents a table in a SQL statement. 
	/// </summary> 
	public class SqlTable : SqlTableExpression
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlTable"/> class using the
		/// specified table name.
		/// </summary>
		/// <param name="tableName">
		/// The name of the table.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="tableName"/> argument is <see langword="null"/>,
		/// an empty string, or a string containing only white-space characters.
		/// </exception>
		public SqlTable(SqlIdentifier tableName)
			: this(tableName, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlTable"/> class using the
		/// specified table name and alias.
		/// </summary>
		/// <param name="tableName">
		/// The name of the table.
		/// </param>
		/// <param name="alias">
		/// The alias to use for the table.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="tableName"/> argument is <see langword="null"/>,
		/// an empty string, or a string containing only white-space characters.
		/// </exception>
		public SqlTable(SqlIdentifier tableName, string alias)
			: base(alias)
		{
			if (tableName == null) throw new ArgumentNullException(nameof(tableName));

			TableName = tableName;
		}

		/// <summary>
		/// Gets the name of the table.
		/// </summary>
		public SqlIdentifier TableName
		{
			get;
		}

		/// <summary> 
		/// Implicitly converts a <see cref="string"/> representing a table name to a <see cref="SqlTable"/> instance. 
		/// </summary> 
		/// <param name="tableName"> 
		/// The name of the table. 
		/// </param> 
		/// <returns> 
		/// A <see cref="SqlTable"/> instance representing the specified table. 
		/// </returns> 
		public static implicit operator SqlTable(string tableName)
		{
			return new SqlTable(tableName);
		}
		
		/// <summary> 
		/// Converts the specified column name to a <see cref="SqlColumn"/> when concatenated 
		/// with a <see cref="SqlTable"/>. 
		/// </summary> 
		/// <param name="table"> 
		/// The table used to access the column. 
		/// </param> 
		/// <param name="columnName"> 
		/// The name of the column. 
		/// </param> 
		/// <returns> 
		/// A <see cref="SqlColumn"/> instance representing the specified column in the specified table. 
		/// </returns> 
		public static SqlColumn operator +(SqlTable table, string columnName)
		{
			SqlIdentifier identifier;
			if (table.Alias != null)
			{
				identifier = new SqlIdentifier(new[] { table.Alias, columnName });
			}
			else
			{
				var segments = table.TableName.Segments;
				identifier = new SqlIdentifier(segments.Concat(new[] { columnName }).ToArray());
			}
			return new SqlColumn(identifier, null);
		}

	}
}
