using Ninject.Components;
using System;
using System.Collections.Generic;
namespace Ninject.Modules
{
	public interface IModuleLoaderPlugin : INinjectComponent, IDisposable
	{
		IEnumerable<string> SupportedExtensions
		{
			get;
		}
		void LoadModules(IEnumerable<string> filenames);
	}
}
