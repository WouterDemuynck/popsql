using System;
using System.Collections.Generic;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL SELECT statement.
	/// </summary>
	public class SqlSelect : SqlStatement
	{
		private List<SqlJoin> _joins;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlSelect"/> class using the
		/// specified <paramref name="columns"/>.
		/// </summary>
		/// <param name="columns">
		/// The columns selected by the new <see cref="SqlSelect"/> instance.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="columns"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlSelect(IEnumerable<SqlColumn> columns)
		{
			if (columns == null) throw new ArgumentNullException("columns");
			Columns = columns;
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
		{
			get
			{
				return SqlExpressionType.Select;
			}
		}

		/// <summary>
		/// Gets the table from which rows are selected by this SQL SELECT statement.
		/// </summary>
		public SqlTable Table
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the collection of columns selected by this SQL SELECT statement.
		/// </summary>
		public IEnumerable<SqlColumn> Columns
		{
			get;
			private set;
		}

		/// <summary> 
		/// Gets the predicate determining which rows are selected by this SQL SELECT statement. 
		/// </summary> 
		public SqlExpression Predicate
		{
			get;
			private set;
		}

		/// <summary>
		/// Sets the table from which rows are selected by this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table from which rows are selected.
		/// </param>
		/// <returns>
		/// The current instance of the <see cref="SqlSelect"/> class.
		/// </returns>
		public SqlSelect From(SqlTable table)
		{
			if (table == null) throw new ArgumentNullException("table");
			Table = table;
			return this;
		}

		/// <summary> 
		/// Sets the predicate used for determining which rows are selected by this SQL SELECT statement. 
		/// </summary> 
		/// <param name="predicate"> 
		/// The predicate used for determining which rows are selected by this SQL SELECT statement. 
		/// </param> 
		/// <returns> 
		/// The current instance of the <see cref="SqlSelect"/> class. 
		/// </returns> 
		public SqlSelect Where(SqlExpression predicate)
		{
			Predicate = predicate;
			return this;
		}

		/// <summary>
		/// Gets the joins used by this <see cref="SqlSelect"/>.
		/// </summary>
		public IEnumerable<SqlJoin> Joins
		{
			get
			{
				return _joins ?? (_joins = new List<SqlJoin>());
			}
		}

		/// <summary>
		/// Adds a SQL JOIN clause to this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table with which to join.
		/// </param>
		/// <param name="predicate">
		/// The predicate used for determining which rows are joined by this JOIN clause.
		/// </param>
		/// <returns>
		/// The current instance of the <see cref="SqlSelect"/> class.
		/// </returns>
		public SqlSelect Join(SqlTable table, SqlExpression predicate = null)
		{
			return JoinInternal(SqlJoinType.Default, table, predicate);
		}

		/// <summary>
		/// Adds a SQL INNER JOIN clause to this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table with which to join.
		/// </param>
		/// <param name="predicate">
		/// The predicate used for determining which rows are joined by this JOIN clause.
		/// </param>
		/// <returns>
		/// The current instance of the <see cref="SqlSelect"/> class.
		/// </returns>
		public SqlSelect InnerJoin(SqlTable table, SqlExpression predicate = null)
		{
			return JoinInternal(SqlJoinType.Inner, table, predicate);
		}

		/// <summary>
		/// Adds a SQL LEFT JOIN clause to this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table with which to join.
		/// </param>
		/// <param name="predicate">
		/// The predicate used for determining which rows are joined by this JOIN clause.
		/// </param>
		/// <returns>
		/// The current instance of the <see cref="SqlSelect"/> class.
		/// </returns>
		public SqlSelect LeftJoin(SqlTable table, SqlExpression predicate = null)
		{
			return JoinInternal(SqlJoinType.Left, table, predicate);
		}

		/// <summary>
		/// Adds a SQL RIGHT JOIN clause to this SQL SELECT statement.
		/// </summary>
		/// <param name="table">
		/// The table with which to join.
		/// </param>
		/// <param name="predicate">
		/// The predicate used for determining which rows are joined by this JOIN clause.
		/// </param>
		/// <returns>
		/// The current instance of the <see cref="SqlSelect"/> class.
		/// </returns>
		public SqlSelect RightJoin(SqlTable table, SqlExpression predicate = null)
		{
			return JoinInternal(SqlJoinType.Right, table, predicate);
		}

		private SqlSelect JoinInternal(SqlJoinType type, SqlTable table, SqlExpression predicate = null)
		{
			if (table == null) throw new ArgumentNullException("table");
			if (_joins == null)
			{
				_joins = new List<SqlJoin>();
			}
			_joins.Add(new SqlJoin(type, table, predicate));
			return this;
		}
	}
}
