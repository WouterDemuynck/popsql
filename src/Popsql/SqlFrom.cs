using System;
using System.Collections.Generic;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL FROM statement beloning to a specific SQL statement.
	/// </summary>
	public class SqlFrom : SqlClause
	{
		private List<SqlJoin> _joins;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlFrom"/> class.
		/// </summary>
		public SqlFrom(SqlTableExpression table)
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			Table = table;
		}

		/// <summary>
		/// Gets the <see cref="SqlTableExpression"/> from which rows are selected.
		/// </summary>
		public SqlTableExpression Table
		{
			get;
		}

		/// <summary>
		/// Gets the joins used by this <see cref="SqlFrom"/>.
		/// </summary>
		public IReadOnlyCollection<SqlJoin> Joins
			=> _joins ?? (_joins = new List<SqlJoin>());

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.From;

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
			Table.Accept(visitor);
			Joins.ForEach(join => join.Accept(visitor));
		}

		internal void AddJoin(SqlJoin join)
		{
			if (_joins == null)
			{
				_joins = new List<SqlJoin>();
			}
			_joins.Add(join);
		}
	}
}