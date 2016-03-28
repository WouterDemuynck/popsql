﻿using System;
using System.Collections.Generic;
using Popsql.Grammar;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL UPDATE statement.
	/// </summary>
	public partial class SqlUpdate : SqlStatement
	{
		private List<SqlAssign> _values;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlUpdate"/> class using
		/// the specified <paramref name="table"/>.
		/// </summary>
		/// <param name="table">
		/// The table updated by the new <see cref="SqlUpdate"/>.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="table"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlUpdate(SqlTable table)
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			Table = table;
		}

		/// <summary>
		/// Gets the table updated by this <see cref="SqlUpdate" />.
		/// </summary>
		public SqlTable Table
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the values assigned by this <see cref="SqlUpdate"/>.
		/// </summary>
		public IEnumerable<SqlAssign> Values 
			=> _values ?? (_values = new List<SqlAssign>());

		/// <summary>
		/// Gets the predicate determining which rows are updated by this SQL UPDATE statement.
		/// </summary>
		public SqlExpression Where
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Update;
	}
}