using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
namespace Ninject.Planning.Bindings
{
	public class Binding : IBinding
	{
		public Type Service
		{
			get;
			private set;
		}
		public IBindingMetadata Metadata
		{
			get;
			private set;
		}
		public bool IsImplicit
		{
			get;
			set;
		}
		public bool IsConditional
		{
			get
			{
				return this.Condition != null;
			}
		}
		public BindingTarget Target
		{
			get;
			set;
		}
		public Func<IRequest, bool> Condition
		{
			get;
			set;
		}
		public Func<IContext, IProvider> ProviderCallback
		{
			get;
			set;
		}
		public Func<IContext, object> ScopeCallback
		{
			get;
			set;
		}
		public ICollection<IParameter> Parameters
		{
			get;
			private set;
		}
		public ICollection<Action<object>> ActivationActions
		{
			get;
			private set;
		}
		public ICollection<Action<object>> DeactivationActions
		{
			get;
			private set;
		}
		public Binding(Type service) : this(service, new BindingMetadata())
		{
		}
		public Binding(Type service, IBindingMetadata metadata)
		{
			Ensure.ArgumentNotNull(service, "service");
			Ensure.ArgumentNotNull(metadata, "metadata");
			this.Service = service;
			this.Metadata = metadata;
			this.Parameters = new List<IParameter>();
			this.ActivationActions = new List<Action<object>>();
			this.DeactivationActions = new List<Action<object>>();
			this.ScopeCallback = StandardScopeCallbacks.Transient;
		}
		public IProvider GetProvider(IContext context)
		{
			Ensure.ArgumentNotNull(context, "context");
			return this.ProviderCallback(context);
		}
		public object GetScope(IContext context)
		{
			Ensure.ArgumentNotNull(context, "context");
			return this.ScopeCallback(context);
		}
		public bool Matches(IRequest request)
		{
			Ensure.ArgumentNotNull(request, "request");
			return this.Condition == null || this.Condition(request);
		}
	}
}
