using Ninject.Components;
using Ninject.Infrastructure;
using System;
using System.Collections.Generic;
namespace Ninject.Planning.Bindings.Resolvers
{
	public interface IBindingResolver : INinjectComponent, IDisposable
	{
		IEnumerable<IBinding> Resolve(Multimap<Type, IBinding> bindings, Type service);
	}
}
