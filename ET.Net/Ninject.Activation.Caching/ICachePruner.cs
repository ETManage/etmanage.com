using Ninject.Components;
using System;
namespace Ninject.Activation.Caching
{
	public interface ICachePruner : INinjectComponent, IDisposable
	{
		void Start(ICache cache);
		void Stop();
	}
}
