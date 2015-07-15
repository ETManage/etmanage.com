using Ninject.Activation.Caching;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Introspection;
using Ninject.Parameters;
using Ninject.Planning;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace Ninject.Activation
{
	public class Context : IContext
	{
		public IKernel Kernel
		{
			get;
			set;
		}
		public IRequest Request
		{
			get;
			set;
		}
		public IBinding Binding
		{
			get;
			set;
		}
		public IPlan Plan
		{
			get;
			set;
		}
		public ICollection<IParameter> Parameters
		{
			get;
			set;
		}
		public Type[] GenericArguments
		{
			get;
			private set;
		}
		public bool HasInferredGenericArguments
		{
			get;
			private set;
		}
		public ICache Cache
		{
			get;
			private set;
		}
		public IPlanner Planner
		{
			get;
			private set;
		}
		public IPipeline Pipeline
		{
			get;
			private set;
		}
		public Context(IKernel kernel, IRequest request, IBinding binding, ICache cache, IPlanner planner, IPipeline pipeline)
		{
			Ensure.ArgumentNotNull(kernel, "kernel");
			Ensure.ArgumentNotNull(request, "request");
			Ensure.ArgumentNotNull(binding, "binding");
			Ensure.ArgumentNotNull(cache, "cache");
			Ensure.ArgumentNotNull(planner, "planner");
			Ensure.ArgumentNotNull(pipeline, "pipeline");
			this.Kernel = kernel;
			this.Request = request;
			this.Binding = binding;
			this.Parameters = request.Parameters.Union(binding.Parameters).ToList<IParameter>();
			this.Cache = cache;
			this.Planner = planner;
			this.Pipeline = pipeline;
			if (binding.Service.IsGenericTypeDefinition)
			{
				this.HasInferredGenericArguments = true;
				this.GenericArguments = request.Service.GetGenericArguments();
			}
		}
		public object GetScope()
		{
			return this.Request.GetScope() ?? this.Binding.GetScope(this);
		}
		public IProvider GetProvider()
		{
			return this.Binding.GetProvider(this);
		}
		public object Resolve()
		{
			IBinding binding;
			Monitor.Enter(binding = this.Binding);
			object result;
			try
			{
				if (this.Request.ActiveBindings.Contains(this.Binding))
				{
					throw new ActivationException(ExceptionFormatter.CyclicalDependenciesDetected(this));
				}
				object obj = this.Cache.TryGet(this);
				if (obj != null)
				{
					result = obj;
				}
				else
				{
					this.Request.ActiveBindings.Push(this.Binding);
					InstanceReference instanceReference = new InstanceReference
					{
						Instance = this.GetProvider().Create(this)
					};
					this.Request.ActiveBindings.Pop();
					if (this.GetScope() != null)
					{
						this.Cache.Remember(this, instanceReference);
					}
					if (this.Plan == null)
					{
						this.Plan = this.Planner.GetPlan(instanceReference.Instance.GetType());
					}
					this.Pipeline.Activate(this, instanceReference);
					result = instanceReference.Instance;
				}
			}
			finally
			{
				Monitor.Exit(binding);
			}
			return result;
		}
	}
}
