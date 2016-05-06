using System;
using System.Collections;
using System.Collections.Generic;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL SET clause.
	/// </summary>
	public class SqlSet : SqlClause, IReadOnlyCollection<SqlAssign>
	{
		private readonly List<SqlAssign> _assignExpressions;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlSet"/> class.
		/// </summary>
		public SqlSet()
		{
			_assignExpressions = new List<SqlAssign>();
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.Set;

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// An enumerator that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<SqlAssign> GetEnumerator()
		{
			return _assignExpressions.GetEnumerator();
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
		/// Gets the number of assignment expressions in this SQL SET clause.
		/// </summary>
		public int Count
			=> _assignExpressions.Count;

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

			_assignExpressions.Accept(visitor);
		}

		internal void Add(SqlAssign assignExpression)
		{
			if (assignExpression == null) throw new ArgumentNullException(nameof(assignExpression));
			_assignExpressions.Add(assignExpression);
		}
	}
}