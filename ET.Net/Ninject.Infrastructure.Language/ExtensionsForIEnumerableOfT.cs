using System;
using System.Collections.Generic;
namespace Ninject.Infrastructure.Language
{
	internal static class ExtensionsForIEnumerableOfT
	{
		public static void Map<T>(this IEnumerable<T> series, Action<T> action)
		{
			foreach (T current in series)
			{
				action(current);
			}
		}
	}
}
