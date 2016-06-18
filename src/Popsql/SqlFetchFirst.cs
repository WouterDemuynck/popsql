using System;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL FETCH FIRST clause.
	/// </summary>
	public class SqlFetchFirst : SqlClause
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlFetchFirst"/> class using the specified row offset.
		/// </summary>
		/// <param name="offset">
		/// The row offset at which to start fetching rows.
		/// </param>
		/// <param name="count">
		/// The number of rows to fetch.
		/// </param>
		public SqlFetchFirst(int offset, int count)
		{
			if (offset < 0) throw new ArgumentException(
				$"The {nameof(offset)} argument must have a value greater than or equal to 0.",
				nameof(offset));

			if (count < 1) throw new ArgumentException(
				$"The {nameof(count)} argument must have a value greater than or equal to 1.",
				nameof(count));

			Offset = offset;
			Count = count;
		}

		internal SqlFetchFirst(int offset)
			: this(offset, 1)
		{
		}

		/// <summary>
		/// Gets the offset at which rows are being fetched.
		/// </summary>
		public int Offset
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the number of rows to fetch.
		/// </summary>
		public int Count
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.FetchFirst;
	}
}