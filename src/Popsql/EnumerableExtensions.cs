using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Popsql
{
	internal static class EnumerableExtensions
	{
		public static IReadOnlyCollection<T> ToReadOnly<T>(this IEnumerable<T> source)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));

			return new ReadOnlyCollection<T>(source.ToList());
		}

		public static void IfAny<T>(this IEnumerable<T> source, Action<IEnumerable<T>> action)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (action == null) return;

			if (source.Any())
			{
				action(source);
			}
		}

		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (action == null) return;

			foreach (var item in source)
			{
				action(item);
			}
		}
		public static void For<T>(this IEnumerable<T> source, Action<int, T> action)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (action == null) return;

			int index = 0;
			foreach (var item in source)
			{
				action(index++, item);
			}
		}
	}
}