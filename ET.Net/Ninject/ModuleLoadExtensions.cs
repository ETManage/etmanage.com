using Ninject.Infrastructure;
using Ninject.Modules;
using System;
using System.Reflection;
namespace Ninject
{
	public static class ModuleLoadExtensions
	{
		public static void Load<TModule>(this IKernel kernel) where TModule : INinjectModule, new()
		{
			Ensure.ArgumentNotNull(kernel, "kernel");
			kernel.Load(new INinjectModule[]
			{
				(default(TModule) == null) ? Activator.CreateInstance<TModule>() : default(TModule)
			});
		}
		public static void Load(this IKernel kernel, params INinjectModule[] modules)
		{
			kernel.Load(modules);
		}
		public static void Load(this IKernel kernel, params string[] filePatterns)
		{
			kernel.Load(filePatterns);
		}
		public static void Load(this IKernel kernel, params Assembly[] assemblies)
		{
			kernel.Load(assemblies);
		}
	}
}
