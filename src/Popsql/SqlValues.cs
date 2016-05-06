using System;
using System.Collections;
using System.Collections.Generic;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL VALUES clause.
	/// </summary>
	public class SqlValues : SqlClause, IReadOnlyCollection<IEnumerable<SqlValue>>
	{
		private readonly List<IEnumerable<SqlValue>> _values;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlValues"/> class.
		/// </summary>
		public SqlValues()
		{
			_values = new List<IEnumerable<SqlValue>>();
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.Values;

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// An enumerator that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<IEnumerable<SqlValue>> GetEnumerator()
		{
			return _values.GetEnumerator();
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
		/// Gets the number of value sets in this SQL VALUES clause.
		/// </summary>
		public int Count
			=> _values.Count;

		internal void Add(IEnumerable<SqlValue> values)
		{
			if (values == null) throw new ArgumentNullException(nameof(values));
			_values.Add(values);
		}

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
			_values.Accept(visitor);
		}
	}
}