using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Planning.Targets;
using System;
using System.Collections.Generic;
namespace Ninject.Activation
{
	public interface IRequest
	{
		Type Service
		{
			get;
		}
		IRequest ParentRequest
		{
			get;
		}
		IContext ParentContext
		{
			get;
		}
		ITarget Target
		{
			get;
		}
		Func<IBindingMetadata, bool> Constraint
		{
			get;
		}
		ICollection<IParameter> Parameters
		{
			get;
		}
		Stack<IBinding> ActiveBindings
		{
			get;
		}
		int Depth
		{
			get;
		}
		bool IsOptional
		{
			get;
			set;
		}
		bool IsUnique
		{
			get;
			set;
		}
		bool Matches(IBinding binding);
		object GetScope();
		IRequest CreateChild(Type service, IContext parentContext, ITarget target);
	}
}
