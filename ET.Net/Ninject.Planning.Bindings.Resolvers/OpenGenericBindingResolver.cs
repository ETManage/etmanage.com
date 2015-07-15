using Ninject.Components;
using Ninject.Infrastructure;
using System;
using System.Collections.Generic;
namespace Ninject.Planning.Bindings.Resolvers
{
	public class OpenGenericBindingResolver : NinjectComponent, IBindingResolver, INinjectComponent, IDisposable
	{
		public IEnumerable<IBinding> Resolve(Multimap<Type, IBinding> bindings, Type service)
		{
			if (service.IsGenericType && bindings.ContainsKey(service.GetGenericTypeDefinition()))
			{
				Type genericTypeDefinition = service.GetGenericTypeDefinition();
				foreach (IBinding current in bindings[genericTypeDefinition])
				{
					yield return current;
				}
			}
			yield break;
		}
	}
}
