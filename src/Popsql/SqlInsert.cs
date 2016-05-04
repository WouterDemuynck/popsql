using System.Collections.Generic;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL INSERT INTO statement.
	/// </summary>
	public partial class SqlInsert : SqlStatement
	{
		private SqlValues _values;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlInsert"/> class.
		/// </summary>
		public SqlInsert()
		{
		}

		/// <summary>
		/// Gets the table into which this <see cref="SqlInsert" /> inserts rows.
		/// </summary>
		public SqlInto Into
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
		public SqlValues Values
			=> _values = (_values ?? new SqlValues());

		/// <summary>
		/// Returns the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Insert;

		public override void Accept(ISqlVisitor visitor)
		{
			base.Accept(visitor);

			Into?.Accept(visitor);
			Columns?.Accept(visitor);
			Values?.Accept(visitor);
		}
	}
}