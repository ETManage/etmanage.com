using Ninject.Activation;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;
namespace Ninject.Syntax
{
	public interface IResolutionRoot
	{
		bool CanResolve(IRequest request);
		IEnumerable<object> Resolve(IRequest request);
		IRequest CreateRequest(Type service, Func<IBindingMetadata, bool> constraint, IEnumerable<IParameter> parameters, bool isOptional, bool isUnique);
	}
}
