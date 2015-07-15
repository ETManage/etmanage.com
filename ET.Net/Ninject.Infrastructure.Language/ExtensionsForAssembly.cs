using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Ninject.Infrastructure.Language
{
	internal static class ExtensionsForAssembly
	{
		public static bool HasNinjectModules(this Assembly assembly)
		{
			return assembly.GetExportedTypes().Any(new Func<Type, bool>(ExtensionsForAssembly.IsLoadableModule));
		}
		public static IEnumerable<INinjectModule> GetNinjectModules(this Assembly assembly)
		{
			foreach (Type current in assembly.GetExportedTypes().Where(new Func<Type, bool>(ExtensionsForAssembly.IsLoadableModule)))
			{
				yield return Activator.CreateInstance(current) as INinjectModule;
			}
			yield break;
		}
		private static bool IsLoadableModule(Type type)
		{
			return typeof(INinjectModule).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface && type.GetConstructor(Type.EmptyTypes) != null;
		}
	}
}
