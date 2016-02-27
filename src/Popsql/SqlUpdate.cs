using System;
using System.Collections.Generic;
using Popsql.Grammar;

namespace Popsql
{
	public class SqlUpdate : SqlStatement, ISqlUpdateClause, ISqlSetClause, ISqlWhereClause<SqlUpdate>
	{
		private List<SqlAssign> _values;

		public SqlUpdate(SqlTable table)
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			Table = table;
		}

		public SqlTable Table
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the values assigned by this <see cref="SqlUpdate"/>.
		/// </summary>
		public IEnumerable<SqlAssign> Values
		{
			get
			{
				return _values ?? (_values = new List<SqlAssign>());
			}
		}

		/// <summary>
		/// Gets the predicate determining which rows are updated by this SQL UPDATE statement.
		/// </summary>
		public SqlExpression Where
		{
			get;
			private set;
		}

		public override SqlExpressionType ExpressionType
		{
			get
			{
				return SqlExpressionType.Update;
			}
		}

		ISqlSetClause ISqlUpdateClause.Set(SqlColumn column, SqlValue value)
		{
			if (column == null) throw new ArgumentNullException(nameof(column));
			if (value == null) value = SqlConstant.Null;

			if (_values == null)
			{
				_values = new List<SqlAssign>();
			}
			_values.Add(new SqlAssign(column, value));
			return this;
		}

		ISqlWhereClause<SqlUpdate> ISqlSetClause.Where(SqlExpression predicate)
		{
			Where = predicate;
			return this;
		}

		SqlUpdate ISqlGo<SqlUpdate>.Go()
		{
			return this;
		}
	}
}