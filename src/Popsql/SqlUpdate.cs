using System;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL UPDATE statement.
	/// </summary>
	public partial class SqlUpdate : SqlStatement
	{
		private SqlSet _set;

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
		}

		/// <summary>
		/// Gets the values assigned by this <see cref="SqlUpdate"/>.
		/// </summary>
		public SqlSet Set
			=> _set = (_set ?? new SqlSet());

		/// <summary>
		/// Gets the predicate determining which rows are updated by this SQL UPDATE statement.
		/// </summary>
		public SqlWhere Where
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Update;

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
			Set.Accept(visitor);
			Where?.Accept(visitor);
		}
	}
}