using System;
using System.Collections.Generic;

namespace Popsql.Visitors
{
	/// <summary>
	/// Provides extension methods that allow accepting visitors on specific collection types.s
	/// </summary>
	public static class SqlVisitorExtensions
	{
		/// <summary>
		/// Accepts the specified <paramref name="visitor"/> and dispatches calls to the specific visitor
		/// methods for this object.
		/// </summary>
		/// <param name="source">
		/// The collection of <see cref="SqlColumn"/> expressions on which to accept the visitor.
		/// </param>
		/// <param name="visitor">
		/// The <see cref="ISqlVisitor" /> to visit this object with.
		/// </param>
		public static void Accept(this IEnumerable<SqlColumn> source, ISqlVisitor visitor)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(source);
		}

		/// <summary>
		/// Accepts the specified <paramref name="visitor"/> and dispatches calls to the specific visitor
		/// methods for this object.
		/// </summary>
		/// <param name="source">
		/// The collection of <see cref="SqlValue"/> expression sets on which to accept the visitor.
		/// </param>
		/// <param name="visitor">
		/// The <see cref="ISqlVisitor" /> to visit this object with.
		/// </param>
		public static void Accept(this IEnumerable<IEnumerable<SqlValue>> source, ISqlVisitor visitor)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(source);
		}

		/// <summary>
		/// Accepts the specified <paramref name="visitor"/> and dispatches calls to the specific visitor
		/// methods for this object.
		/// </summary>
		/// <param name="source">
		/// The collection of <see cref="SqlValue"/> expressions on which to accept the visitor.
		/// </param>
		/// <param name="visitor">
		/// The <see cref="ISqlVisitor" /> to visit this object with.
		/// </param>
		public static void Accept(this IEnumerable<SqlValue> source, ISqlVisitor visitor)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(source);
		}

		/// <summary>
		/// Accepts the specified <paramref name="visitor"/> and dispatches calls to the specific visitor
		/// methods for this object.
		/// </summary>
		/// <param name="source">
		/// The collection of <see cref="SqlSort"/> expressions on which to accept the visitor.
		/// </param>
		/// <param name="visitor">
		/// The <see cref="ISqlVisitor" /> to visit this object with.
		/// </param>
		public static void Accept(this IEnumerable<SqlSort> source, ISqlVisitor visitor)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(source);
		}

		/// <summary>
		/// Accepts the specified <paramref name="visitor"/> and dispatches calls to the specific visitor
		/// methods for this object.
		/// </summary>
		/// <param name="source">
		/// The collection of <see cref="SqlAssign"/> expressions on which to accept the visitor.
		/// </param>
		/// <param name="visitor">
		/// The <see cref="ISqlVisitor" /> to visit this object with.
		/// </param>
		public static void Accept(this IEnumerable<SqlAssign> source, ISqlVisitor visitor)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(source);
		}

		/// <summary>
		/// Accepts the specified <paramref name="visitor"/> and dispatches calls to the specific visitor
		/// methods for this object.
		/// </summary>
		/// <param name="operator">
		/// The <see cref="SqlBinaryOperator"/> on which to accept the visitor.
		/// </param>
		/// <param name="visitor">
		/// The <see cref="ISqlVisitor" /> to visit this object with.
		/// </param>
		public static void Accept(this SqlBinaryOperator @operator, ISqlVisitor visitor)
		{
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(@operator);
		}

		/// <summary>
		/// Accepts the specified <paramref name="visitor"/> and dispatches calls to the specific visitor
		/// methods for this object.
		/// </summary>
		/// <param name="sortOrder">
		/// The <see cref="SqlSortOrder"/> on which to accept the visitor.
		/// </param>
		/// <param name="visitor">
		/// The <see cref="ISqlVisitor" /> to visit this object with.
		/// </param>
		public static void Accept(this SqlSortOrder sortOrder, ISqlVisitor visitor)
		{
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(sortOrder);
		}
	}
}