using System;
using System.Collections.Generic;

namespace Popsql
{
	internal static class EnumerableExtensions
	{
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