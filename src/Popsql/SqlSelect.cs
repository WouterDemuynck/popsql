using System;
using System.Collections.Generic;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL SELECT statement.
	/// </summary>
	public partial class SqlSelect : SqlStatement
	{
		private List<SqlJoin> _joins;
		private List<SqlSort> _orderBy;

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
			if (columns == null) throw new ArgumentNullException(nameof(columns));
			Select = columns;
		}

		/// <summary>
		/// Gets the collection of values selected by this SQL SELECT statement.
		/// </summary>
		public IEnumerable<SqlValue> Select
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the table from which rows are selected by this SQL SELECT statement.
		/// </summary>
		public SqlTableExpression From
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the joins used by this <see cref="SqlSelect"/>.
		/// </summary>
		public IReadOnlyCollection<SqlJoin> Joins 
			=> _joins ?? (_joins = new List<SqlJoin>());

		/// <summary>
		/// Gets the predicate determining which rows are selected by this SQL SELECT statement.
		/// </summary>
		public SqlExpression Where
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the collection of sorting clauses determining the result ordering of this SQL SELECT statement.
		/// </summary>
		public IReadOnlyCollection<SqlSort> OrderBy 
			=> _orderBy ?? (_orderBy = new List<SqlSort>());

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Select;

		/// <summary>
		/// Implicitly converts this <see cref="SqlSelect"/> to a sub query by assigning
		/// an alias.
		/// </summary>
		/// <param name="query">
		/// The <see cref="SqlSelect"/> to convert to a <see cref="SqlSubquery"/>.
		/// </param>
		/// <param name="alias">
		/// The alias assigned to the <see cref="SqlSubquery"/>.
		/// </param>
		/// <returns>
		/// An instance of the <see cref="SqlSubquery"/> class representing the specified
		/// <paramref name="query"/> with the specified <paramref name="alias"/>.
		/// </returns>
		public static SqlSubquery operator +(SqlSelect query, string alias)
		{
			return new SqlSubquery(query, alias);
		}
	}
}