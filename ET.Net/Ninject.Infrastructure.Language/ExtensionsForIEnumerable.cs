using System;
using System.Collections;
using System.Linq;
using System.Reflection;
namespace Ninject.Infrastructure.Language
{
	internal static class ExtensionsForIEnumerable
	{
		public static IEnumerable CastSlow(this IEnumerable series, Type elementType)
		{
			MethodInfo methodInfo = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(new Type[]
			{
				elementType
			});
			return methodInfo.Invoke(null, new IEnumerable[]
			{
				series
			}) as IEnumerable;
		}
		public static Array ToArraySlow(this IEnumerable series, Type elementType)
		{
			MethodInfo methodInfo = typeof(Enumerable).GetMethod("ToArray").MakeGenericMethod(new Type[]
			{
				elementType
			});
			return methodInfo.Invoke(null, new IEnumerable[]
			{
				series
			}) as Array;
		}
		public static IList ToListSlow(this IEnumerable series, Type elementType)
		{
			MethodInfo methodInfo = typeof(Enumerable).GetMethod("ToList").MakeGenericMethod(new Type[]
			{
				elementType
			});
			return methodInfo.Invoke(null, new IEnumerable[]
			{
				series
			}) as IList;
		}
	}
}
