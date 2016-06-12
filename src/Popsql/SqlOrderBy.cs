using System;
using System.Collections;
using System.Collections.Generic;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL ORDER BY clause.
	/// </summary>
	public class SqlOrderBy : SqlClause, IReadOnlyCollection<SqlSort>
	{
		private readonly List<SqlSort> _sortExpressions;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlOrderBy"/> class.
		/// </summary>
		public SqlOrderBy()
		{
			_sortExpressions = new List<SqlSort>();
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.OrderBy;

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// An enumerator that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<SqlSort> GetEnumerator()
		{
			return _sortExpressions.GetEnumerator();
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
			=> _sortExpressions.Count;

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

			_sortExpressions.Accept(visitor);
		}

		internal void Add(SqlSort sortExpression)
		{
			if (sortExpression == null) throw new ArgumentNullException(nameof(sortExpression));
			_sortExpressions.Add(sortExpression);
		}
	}
}