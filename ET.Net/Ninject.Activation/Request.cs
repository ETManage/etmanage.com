using Ninject.Infrastructure;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Planning.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Ninject.Activation
{
	public class Request : IRequest
	{
		public Type Service
		{
			get;
			private set;
		}
		public IRequest ParentRequest
		{
			get;
			private set;
		}
		public IContext ParentContext
		{
			get;
			private set;
		}
		public ITarget Target
		{
			get;
			private set;
		}
		public Func<IBindingMetadata, bool> Constraint
		{
			get;
			private set;
		}
		public ICollection<IParameter> Parameters
		{
			get;
			private set;
		}
		public Stack<IBinding> ActiveBindings
		{
			get;
			private set;
		}
		public int Depth
		{
			get;
			private set;
		}
		public bool IsOptional
		{
			get;
			set;
		}
		public bool IsUnique
		{
			get;
			set;
		}
		public Func<object> ScopeCallback
		{
			get;
			private set;
		}
		public Request(Type service, Func<IBindingMetadata, bool> constraint, IEnumerable<IParameter> parameters, Func<object> scopeCallback, bool isOptional, bool isUnique)
		{
			Ensure.ArgumentNotNull(service, "service");
			Ensure.ArgumentNotNull(parameters, "parameters");
			this.Service = service;
			this.Constraint = constraint;
			this.Parameters = parameters.ToList<IParameter>();
			this.ScopeCallback = scopeCallback;
			this.ActiveBindings = new Stack<IBinding>();
			this.Depth = 0;
			this.IsOptional = isOptional;
			this.IsUnique = isUnique;
		}
		public Request(IContext parentContext, Type service, ITarget target, Func<object> scopeCallback)
		{
			Ensure.ArgumentNotNull(parentContext, "parentContext");
			Ensure.ArgumentNotNull(service, "service");
			Ensure.ArgumentNotNull(target, "target");
			this.ParentContext = parentContext;
			this.ParentRequest = parentContext.Request;
			this.Service = service;
			this.Target = target;
			this.Constraint = target.Constraint;
			this.IsOptional = target.IsOptional;
			this.Parameters = (
				from p in parentContext.Parameters
				where p.ShouldInherit
				select p).ToList<IParameter>();
			this.ScopeCallback = scopeCallback;
			this.ActiveBindings = new Stack<IBinding>(this.ParentRequest.ActiveBindings);
			this.Depth = this.ParentRequest.Depth + 1;
		}
		public bool Matches(IBinding binding)
		{
			return this.Constraint == null || this.Constraint(binding.Metadata);
		}
		public object GetScope()
		{
			if (this.ScopeCallback != null)
			{
				return this.ScopeCallback();
			}
			return null;
		}
		public IRequest CreateChild(Type service, IContext parentContext, ITarget target)
		{
			return new Request(parentContext, service, target, this.ScopeCallback);
		}
	}
}
