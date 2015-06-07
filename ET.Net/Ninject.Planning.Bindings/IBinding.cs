using Ninject.Activation;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
namespace Ninject.Planning.Bindings
{
	public interface IBinding
	{
		Type Service
		{
			get;
		}
		IBindingMetadata Metadata
		{
			get;
		}
		BindingTarget Target
		{
			get;
			set;
		}
		bool IsImplicit
		{
			get;
			set;
		}
		bool IsConditional
		{
			get;
		}
		Func<IRequest, bool> Condition
		{
			get;
			set;
		}
		Func<IContext, IProvider> ProviderCallback
		{
			get;
			set;
		}
		Func<IContext, object> ScopeCallback
		{
			get;
			set;
		}
		ICollection<IParameter> Parameters
		{
			get;
		}
		ICollection<Action<object>> ActivationActions
		{
			get;
		}
		ICollection<Action<object>> DeactivationActions
		{
			get;
		}
		IProvider GetProvider(IContext context);
		object GetScope(IContext context);
		bool Matches(IRequest request);
	}
}
