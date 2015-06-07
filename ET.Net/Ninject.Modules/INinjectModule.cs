using Ninject.Infrastructure;
using System;
namespace Ninject.Modules
{
	public interface INinjectModule : IHaveKernel
	{
		string Name
		{
			get;
		}
		void OnLoad(IKernel kernel);
		void OnUnload(IKernel kernel);
	}
}
