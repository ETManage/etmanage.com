using Ninject.Components;
using System;
namespace Ninject.Activation.Caching
{
	public interface ICache : INinjectComponent, IDisposable
	{
		int Count
		{
			get;
		}
		void Remember(IContext context, InstanceReference reference);
		object TryGet(IContext context);
		bool Release(object instance);
		void Prune();
		void Clear(object scope);
		void Clear();
	}
}
