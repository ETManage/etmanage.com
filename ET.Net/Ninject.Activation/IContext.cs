using Ninject.Parameters;
using Ninject.Planning;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;
namespace Ninject.Activation
{
	public interface IContext
	{
		IKernel Kernel
		{
			get;
		}
		IRequest Request
		{
			get;
		}
		IBinding Binding
		{
			get;
		}
		IPlan Plan
		{
			get;
			set;
		}
		ICollection<IParameter> Parameters
		{
			get;
		}
		Type[] GenericArguments
		{
			get;
		}
		bool HasInferredGenericArguments
		{
			get;
		}
		IProvider GetProvider();
		object GetScope();
		object Resolve();
	}
}
