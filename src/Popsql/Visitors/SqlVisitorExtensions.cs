using System;
using System.Collections.Generic;

namespace Popsql.Visitors
{
	public static class SqlVisitorExtensions
	{
		public static void Accept(this IEnumerable<SqlColumn> source, ISqlVisitor visitor)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(source);
		}

		public static void Accept(this IEnumerable<IEnumerable<SqlValue>> source, ISqlVisitor visitor)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(source);
		}

		public static void Accept(this IEnumerable<SqlValue> source, ISqlVisitor visitor)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(source);
		}

		public static void Accept(this IEnumerable<SqlSort> source, ISqlVisitor visitor)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(source);
		}

		public static void Accept(this IEnumerable<SqlAssign> source, ISqlVisitor visitor)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(source);
		}

		public static void Accept(this SqlBinaryOperator @operator, ISqlVisitor visitor)
		{
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(@operator);
		}

		public static void Accept(this SqlSortOrder sortOrder, ISqlVisitor visitor)
		{
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));

			visitor.Visit(sortOrder);
		}
	}
}