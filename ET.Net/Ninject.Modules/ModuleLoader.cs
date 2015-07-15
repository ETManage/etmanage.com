using Ninject.Components;
using Ninject.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Ninject.Modules
{
	public class ModuleLoader : NinjectComponent, IModuleLoader, INinjectComponent, IDisposable
	{
		public IKernel Kernel
		{
			get;
			private set;
		}
		public ModuleLoader(IKernel kernel)
		{
			Ensure.ArgumentNotNull(kernel, "kernel");
			this.Kernel = kernel;
		}
		public void LoadModules(IEnumerable<string> patterns)
		{
			IEnumerable<IModuleLoaderPlugin> all = this.Kernel.Components.GetAll<IModuleLoaderPlugin>();
			IEnumerable<IGrouping<string, string>> enumerable = 
				from filename in patterns.SelectMany((string pattern) => ModuleLoader.GetFilesMatchingPattern(pattern))
				group filename by Path.GetExtension(filename).ToLowerInvariant();
			foreach (IGrouping<string, string> current in enumerable)
			{
				string extension = current.Key;
				IModuleLoaderPlugin moduleLoaderPlugin = (
					from p in all
					where p.SupportedExtensions.Contains(extension)
					select p).FirstOrDefault<IModuleLoaderPlugin>();
				if (moduleLoaderPlugin != null)
				{
					moduleLoaderPlugin.LoadModules(current);
				}
			}
		}
		private static string[] GetFilesMatchingPattern(string pattern)
		{
			string path = ModuleLoader.NormalizePath(Path.GetDirectoryName(pattern));
			string fileName = Path.GetFileName(pattern);
			return Directory.GetFiles(path, fileName);
		}
		private static string NormalizePath(string path)
		{
			if (!Path.IsPathRooted(path))
			{
				path = Path.Combine(ModuleLoader.GetBaseDirectory(), path);
			}
			return Path.GetFullPath(path);
		}
		private static string GetBaseDirectory()
		{
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string relativeSearchPath = AppDomain.CurrentDomain.RelativeSearchPath;
			if (!string.IsNullOrEmpty(relativeSearchPath))
			{
				return Path.Combine(baseDirectory, relativeSearchPath);
			}
			return baseDirectory;
		}
	}
}
