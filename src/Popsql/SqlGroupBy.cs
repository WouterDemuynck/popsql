using System;
using System.Collections;
using System.Collections.Generic;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL GROUP BY clause.
	/// </summary>
	public class SqlGroupBy : SqlClause, IReadOnlyCollection<SqlColumn>
	{
		private readonly List<SqlColumn> _columns;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlGroupBy"/> class.
		/// </summary>
		/// <param name="column"></param>
		public SqlGroupBy(SqlColumn column)
		{
			if (column == null) throw new ArgumentNullException(nameof(column));
			_columns = new List<SqlColumn> { column };
		}

		/// <summary>
		/// Gets the predicate used for determining which rows are grouped by this SQL GROUP BY expression.
		/// </summary>
		public SqlHaving Having
		{
			get;
			internal set;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// An enumerator that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<SqlColumn> GetEnumerator()
		{
			return _columns.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// An enumerator that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Gets the number of sort expressions in this SQL ORDER BY clause.
		/// </summary>
		public int Count
			=> _columns.Count;

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.GroupBy;

		/// <summary>
		/// Accepts the specified <paramref name="visitor"/> and dispatches calls to the specific visitor
		/// methods for this object.
		/// </summary>
		/// <param name="visitor">
		/// The <see cref="ISqlVisitor" /> to visit this object with.
		/// </param>
		public override void Accept(ISqlVisitor visitor)
		{
			base.Accept(visitor);

			_columns.Accept(visitor);
			Having?.Accept(visitor);
		}
	}
}