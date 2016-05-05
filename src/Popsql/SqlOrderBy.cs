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

		public IEnumerator<SqlSort> GetEnumerator()
		{
			return _sortExpressions.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count
			=> _sortExpressions.Count;

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