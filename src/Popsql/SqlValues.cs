using System;
using System.Collections;
using System.Collections.Generic;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// 
	/// </summary>
	public class SqlValues : SqlClause, IReadOnlyCollection<IEnumerable<SqlValue>>
	{
		private readonly List<IEnumerable<SqlValue>> _values;

		public SqlValues()
		{
			_values = new List<IEnumerable<SqlValue>>();
		}

		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.Values;

		public IEnumerator<IEnumerable<SqlValue>> GetEnumerator()
		{
			return _values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count
			=> _values.Count;

		internal void Add(IEnumerable<SqlValue> values)
		{
			if (values == null) throw new ArgumentNullException(nameof(values));
			_values.Add(values);
		}

		public override void Accept(ISqlVisitor visitor)
		{
			base.Accept(visitor);
			_values.Accept(visitor);
		}
	}
}