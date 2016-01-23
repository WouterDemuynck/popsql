using System;
using System.Collections.Generic;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL SELECT statement.
	/// </summary>
	public class SqlSelect : SqlStatement
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlSelect"/> class using the
		/// specified <paramref name="columns"/>.
		/// </summary>
		/// <param name="columns">
		/// The columns selected by the new <see cref="SqlSelect"/> instance.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="columns"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlSelect(IEnumerable<SqlColumn> columns)
		{
			if (columns == null) throw new ArgumentNullException("columns");
			Columns = columns;
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
		{
			get
			{
				return SqlExpressionType.Select;
			}
		}

		/// <summary>
		/// Gets the table from which rows are selected by this SQL SELECT statement.
		/// </summary>
		public SqlTable Table
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the collection of columns selected by this SQL SELECT statement.
		/// </summary>
		public IEnumerable<SqlColumn> Columns
		{
			get;
			private set;
		}

		/// <summary>
		/// Sets the table from which rows are selected by this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table from which rows are selected.
		/// </param>
		/// <returns>
		/// The current instance of the <see cref="SqlSelect"/> class.
		/// </returns>
		public SqlSelect From(SqlTable table)
		{
			if (table == null) throw new ArgumentNullException("table");
			Table = table;
			return this;
		}
	}
}
