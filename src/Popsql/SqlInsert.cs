using System;
using System.Collections.Generic;
using System.Linq;
using Popsql.Grammar;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL INSERT INTO statement.
	/// </summary>
	public class SqlInsert : SqlStatement, ISqlInsertClause, ISqlValuesClause
	{
		private List<IEnumerable<SqlValue>> _values;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlInsert"/> class.
		/// </summary>
		public SqlInsert()
		{
		}

		/// <summary>
		/// Gets the table into which this <see cref="SqlInsert" /> inserts rows.
		/// </summary>
		public SqlTable Into
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the collection of columns into which this <see cref="SqlInsert" /> inserts rows.
		/// </summary>
		public IEnumerable<SqlColumn> Columns
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the collection of values to be inserted by this <see cref="SqlInsert"/>.
		/// </summary>
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

		/// <summary>
		/// Returns the expression type of this expression.
		/// </summary>
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