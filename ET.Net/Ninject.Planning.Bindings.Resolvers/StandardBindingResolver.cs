using Ninject.Components;
using Ninject.Infrastructure;
using System;
using System.Collections.Generic;
namespace Ninject.Planning.Bindings.Resolvers
{
	public class StandardBindingResolver : NinjectComponent, IBindingResolver, INinjectComponent, IDisposable
	{
		public IEnumerable<IBinding> Resolve(Multimap<Type, IBinding> bindings, Type service)
		{
			foreach (IBinding current in bindings[service])
			{
				yield return current;
			}
			yield break;
		}
	}
}
