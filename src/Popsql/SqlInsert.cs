using System;
using System.Collections.Generic;
using System.Linq;
using Popsql.Grammar;

namespace Popsql
{
	public class SqlInsert : SqlStatement, ISqlInsertClause, ISqlValuesClause
	{
		private List<IEnumerable<SqlValue>> _values;

		public SqlInsert()
		{
		}

		public SqlTable Into
		{
			get;
			private set;
		}
		public IEnumerable<SqlColumn> Columns
		{
			get;
			private set;
		}

		public IEnumerable<IEnumerable<SqlValue>> Values
		{
			get
			{
				if (_values == null)
				{
					_values = new List<IEnumerable<SqlValue>>();
				}
				return _values.ToArray();
			}
		}

		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Insert;

		ISqlIntoClause ISqlInsertClause.Into(SqlTable table)
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			Into = table;
			return this;
		}

		ISqlIntoClause ISqlInsertClause.Into(SqlTable table, params SqlColumn[] columns)
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			Into = table;
			Columns = columns;
			return this;
		}

		ISqlValuesClause ISqlIntoClause.Values(params SqlValue[] values)
		{
			if (values == null || !values.Any()) throw new ArgumentNullException(nameof(values));
			if (_values == null)
			{
				_values = new List<IEnumerable<SqlValue>>();
			}
			_values.Add(values);
			return this;
		}

		SqlInsert ISqlGo<SqlInsert>.Go()
		{
			return this;
		}
	}
}