using System;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL LIMIT clause.
	/// </summary>
	public class SqlLimit : SqlClause
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlLimit"/> class using the specified row offset.
		/// </summary>
		/// <param name="offset">
		/// The row offset at which to start fetching rows.
		/// </param>
		/// <param name="count">
		/// The number of rows to fetch.
		/// </param>
		public SqlLimit(int? offset, int? count)
		{
			if (offset != null && offset < 0) throw new ArgumentException(
				$"The {nameof(offset)} argument must have a value greater than or equal to 0.",
				nameof(offset));

			if (count != null && count < 1) throw new ArgumentException(
				$"The {nameof(count)} argument must have a value greater than or equal to 1.",
				nameof(count));

			if (offset == null && count == null) throw new ArgumentException(
				$"Either the {nameof(offset)} or the {nameof(count)} must have a value.",
				nameof(offset));

			Offset = offset;
			Count = count;
		}

		/// <summary>
		/// Gets the offset at which rows are being fetched.
		/// </summary>
		public int? Offset
		{
			get;
		}

		/// <summary>
		/// Gets the number of rows to fetch.
		/// </summary>
		public int? Count
		{
			get;
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.Limit;
	}
}