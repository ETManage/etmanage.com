using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Ninject.Modules
{
	public class CompiledModuleLoaderPlugin : NinjectComponent, IModuleLoaderPlugin, INinjectComponent, IDisposable
	{
		private static readonly string[] Extensions = new string[]
		{
			".dll"
		};
		public IKernel Kernel
		{
			get;
			private set;
		}
		public IEnumerable<string> SupportedExtensions
		{
			get
			{
				return CompiledModuleLoaderPlugin.Extensions;
			}
		}
		public CompiledModuleLoaderPlugin(IKernel kernel)
		{
			Ensure.ArgumentNotNull(kernel, "kernel");
			this.Kernel = kernel;
		}
		public void LoadModules(IEnumerable<string> filenames)
		{
			this.Kernel.Load(
				from name in CompiledModuleLoaderPlugin.FindAssembliesWithModules(filenames)
				select Assembly.Load(name));
		}
		private static IEnumerable<AssemblyName> FindAssembliesWithModules(IEnumerable<string> filenames)
		{
			AppDomain appDomain = CompiledModuleLoaderPlugin.CreateTemporaryAppDomain();
			foreach (string current in filenames)
			{
				Assembly assembly;
				try
				{
					AssemblyName assemblyRef = new AssemblyName
					{
						CodeBase = current
					};
					assembly = appDomain.Load(assemblyRef);
				}
				catch (BadImageFormatException)
				{
					continue;
				}
				if (assembly.HasNinjectModules())
				{
					yield return assembly.GetName();
				}
			}
			AppDomain.Unload(appDomain);
			yield break;
		}
		private static AppDomain CreateTemporaryAppDomain()
		{
			return AppDomain.CreateDomain("NinjectModuleLoader", AppDomain.CurrentDomain.Evidence, AppDomain.CurrentDomain.SetupInformation);
		}
	}
}
