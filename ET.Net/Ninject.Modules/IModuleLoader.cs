using Ninject.Components;
using System;
using System.Collections.Generic;
namespace Ninject.Modules
{
	public interface IModuleLoader : INinjectComponent, IDisposable
	{
		void LoadModules(IEnumerable<string> patterns);
	}
}
