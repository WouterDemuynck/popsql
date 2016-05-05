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

		public IEnumerator<SqlAssign> GetEnumerator()
		{
			return _assignExpressions.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count
			=> _assignExpressions.Count;

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