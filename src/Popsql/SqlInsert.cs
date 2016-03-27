using System.Collections.Generic;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL INSERT INTO statement.
	/// </summary>
	public partial class SqlInsert : SqlStatement
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
	}
}