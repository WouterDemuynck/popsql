using System;

namespace Popsql
{
	/// <summary>
	/// Represents a column in a SQL statement
	/// </summary>
	public class SqlColumn : SqlExpression
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlColumn"/> class using the
		/// specified column name.
		/// </summary>
		/// <param name="columnName">
		/// The name of the column.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="columnName"/> argument is <see langword="null"/>,
		/// an empty string, or a string containing only white-space characters.
		/// </exception>
		public SqlColumn(SqlIdentifier columnName)
			: this(columnName, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlTable"/> class using the
		/// specified column name and alias.
		/// </summary>
		/// <param name="columnName">
		/// The name of the column.
		/// </param>
		/// <param name="alias">
		/// The alias to use for the column.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="columnName"/> argument is <see langword="null"/>,
		/// an empty string, or a string containing only white-space characters.
		/// </exception>
		public SqlColumn(SqlIdentifier columnName, string alias)
		{
			if (columnName == null) throw new ArgumentNullException("columnName");

			ColumnName = columnName;
			Alias = alias;
		}

		/// <summary>
		/// Gets the name of the column.
		/// </summary>
		public SqlIdentifier ColumnName
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the alias used for the column.
		/// </summary>
		public string Alias
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
		{
			get
			{
				return SqlExpressionType.Column;
			}
		}

		/// <summary>
		/// Implicitly converts a <see cref="string"/> representing a column name to a <see cref="SqlColumn"/> instance.
		/// </summary>
		/// <param name="columnName">
		/// The name of the table.
		/// </param>
		/// <returns>
		/// A <see cref="SqlColumn"/> instance representing the specified column.
		/// </returns>
		public static implicit operator SqlColumn(string columnName)
		{
			return new SqlColumn(columnName);
		}
	}
}