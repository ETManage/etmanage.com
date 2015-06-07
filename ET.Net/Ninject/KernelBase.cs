using Ninject.Activation;
using Ninject.Activation.Blocks;
using Ninject.Activation.Caching;
using Ninject.Activation.Providers;
using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Disposal;
using Ninject.Infrastructure.Introspection;
using Ninject.Infrastructure.Language;
using Ninject.Modules;
using Ninject.Parameters;
using Ninject.Planning;
using Ninject.Planning.Bindings;
using Ninject.Planning.Bindings.Resolvers;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
namespace Ninject
{
	public abstract class KernelBase : BindingRoot, IKernel, IBindingRoot, IResolutionRoot, IServiceProvider, IDisposableObject, IDisposable
	{
		private readonly Multimap<Type, IBinding> _bindings = new Multimap<Type, IBinding>();
		private readonly Multimap<Type, IBinding> _bindingCache = new Multimap<Type, IBinding>();
		private readonly Dictionary<string, INinjectModule> _modules = new Dictionary<string, INinjectModule>();
		public INinjectSettings Settings
		{
			get;
			private set;
		}
		public IComponentContainer Components
		{
			get;
			private set;
		}
		protected KernelBase() : this(new ComponentContainer(), new NinjectSettings(), new INinjectModule[0])
		{
		}
		protected KernelBase(params INinjectModule[] modules) : this(new ComponentContainer(), new NinjectSettings(), modules)
		{
		}
		protected KernelBase(INinjectSettings settings, params INinjectModule[] modules) : this(new ComponentContainer(), settings, modules)
		{
		}
		protected KernelBase(IComponentContainer components, INinjectSettings settings, params INinjectModule[] modules)
		{
			Ensure.ArgumentNotNull(components, "components");
			Ensure.ArgumentNotNull(settings, "settings");
			Ensure.ArgumentNotNull(modules, "modules");
			this.Settings = settings;
			this.Components = components;
			components.Kernel = this;
			this.AddComponents();
			OnePerRequestModule.StartManaging(this);
			if (this.Settings.LoadExtensions)
			{
				this.Load(new string[]
				{
					this.Settings.ExtensionSearchPattern
				});
			}
			this.Load(modules);
		}
		public override void Dispose(bool disposing)
		{
			if (disposing && !base.IsDisposed)
			{
				OnePerRequestModule.StopManaging(this);
				if (this.Components != null)
				{
					ICache cache = this.Components.Get<ICache>();
					cache.Clear();
					this.Components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		public override void Unbind(Type service)
		{
			Ensure.ArgumentNotNull(service, "service");
			this._bindings.RemoveAll(service);
			Multimap<Type, IBinding> bindingCache;
			Monitor.Enter(bindingCache = this._bindingCache);
			try
			{
				this._bindingCache.Clear();
			}
			finally
			{
				Monitor.Exit(bindingCache);
			}
		}
		public override void AddBinding(IBinding binding)
		{
			Ensure.ArgumentNotNull(binding, "binding");
			this._bindings.Add(binding.Service, binding);
			Multimap<Type, IBinding> bindingCache;
			Monitor.Enter(bindingCache = this._bindingCache);
			try
			{
				this._bindingCache.Clear();
			}
			finally
			{
				Monitor.Exit(bindingCache);
			}
		}
		public override void RemoveBinding(IBinding binding)
		{
			Ensure.ArgumentNotNull(binding, "binding");
			this._bindings.Remove(binding.Service, binding);
			Multimap<Type, IBinding> bindingCache;
			Monitor.Enter(bindingCache = this._bindingCache);
			try
			{
				this._bindingCache.Clear();
			}
			finally
			{
				Monitor.Exit(bindingCache);
			}
		}
		public bool HasModule(string name)
		{
			Ensure.ArgumentNotNullOrEmpty(name, "name");
			return this._modules.ContainsKey(name);
		}
		public IEnumerable<INinjectModule> GetModules()
		{
			return this._modules.Values.ToArray<INinjectModule>();
		}
		public void Load(IEnumerable<INinjectModule> modules)
		{
			Ensure.ArgumentNotNull(modules, "modules");
			foreach (INinjectModule current in modules)
			{
				INinjectModule existingModule;
				if (this._modules.TryGetValue(current.Name, out existingModule))
				{
					throw new NotSupportedException(ExceptionFormatter.ModuleWithSameNameIsAlreadyLoaded(current, existingModule));
				}
				current.OnLoad(this);
				this._modules.Add(current.Name, current);
			}
		}
		public void Load(IEnumerable<string> filePatterns)
		{
			IModuleLoader moduleLoader = this.Components.Get<IModuleLoader>();
			moduleLoader.LoadModules(filePatterns);
		}
		public void Load(IEnumerable<Assembly> assemblies)
		{
			foreach (Assembly current in assemblies)
			{
				this.Load(current.GetNinjectModules());
			}
		}
		public void Unload(string name)
		{
			Ensure.ArgumentNotNullOrEmpty(name, "name");
			INinjectModule ninjectModule;
			if (!this._modules.TryGetValue(name, out ninjectModule))
			{
				throw new NotSupportedException(ExceptionFormatter.NoModuleLoadedWithTheSpecifiedName(name));
			}
			ninjectModule.OnUnload(this);
			this._modules.Remove(name);
		}
		public virtual void Inject(object instance, params IParameter[] parameters)
		{
			Ensure.ArgumentNotNull(instance, "instance");
			Ensure.ArgumentNotNull(parameters, "parameters");
			Type type = instance.GetType();
			IPlanner planner = this.Components.Get<IPlanner>();
			IPipeline pipeline = this.Components.Get<IPipeline>();
			Binding binding = new Binding(type);
			IRequest request = this.CreateRequest(type, null, parameters, false, false);
			IContext context = this.CreateContext(request, binding);
			context.Plan = planner.GetPlan(type);
			InstanceReference reference = new InstanceReference
			{
				Instance = instance
			};
			pipeline.Activate(context, reference);
		}
		public virtual bool Release(object instance)
		{
			Ensure.ArgumentNotNull(instance, "instance");
			ICache cache = this.Components.Get<ICache>();
			return cache.Release(instance);
		}
		public virtual bool CanResolve(IRequest request)
		{
			Ensure.ArgumentNotNull(request, "request");
			IEnumerable<IBindingResolver> all = this.Components.GetAll<IBindingResolver>();
			return all.SelectMany((IBindingResolver r) => r.Resolve(this._bindings, request.Service)).Any<IBinding>();
		}
		public virtual IEnumerable<object> Resolve(IRequest request)
		{
			Ensure.ArgumentNotNull(request, "request");
			if (request.Service == typeof(IKernel))
			{
				return new KernelBase[]
				{
					this
				};
			}
			if (!this.CanResolve(request) && !this.HandleMissingBinding(request.Service))
			{
				if (request.IsOptional)
				{
					return Enumerable.Empty<object>();
				}
				throw new ActivationException(ExceptionFormatter.CouldNotResolveBinding(request));
			}
			else
			{
				IEnumerable<IBinding> source = 
					from binding in this.GetBindings(request.Service).OrderBy(delegate(IBinding binding)
					{
						if (!binding.IsConditional)
						{
							return 1;
						}
						return 0;
					})
					where binding.Matches(request) && request.Matches(binding)
					select binding;
				if (request.IsUnique && source.Count<IBinding>() > 1)
				{
					throw new ActivationException(ExceptionFormatter.CouldNotUniquelyResolveBinding(request));
				}
				return 
					from binding in source
					select this.CreateContext(request, binding) into context
					select context.Resolve();
			}
		}
		public virtual IRequest CreateRequest(Type service, Func<IBindingMetadata, bool> constraint, IEnumerable<IParameter> parameters, bool isOptional, bool isUnique)
		{
			Ensure.ArgumentNotNull(service, "service");
			Ensure.ArgumentNotNull(parameters, "parameters");
			return new Request(service, constraint, parameters, null, isOptional, isUnique);
		}
		public virtual IEnumerable<IBinding> GetBindings(Type service)
		{
			Ensure.ArgumentNotNull(service, "service");
			Multimap<Type, IBinding> bindingCache;
			Monitor.Enter(bindingCache = this._bindingCache);
			IEnumerable<IBinding> result;
			try
			{
				if (!this._bindingCache.ContainsKey(service))
				{
					IEnumerable<IBindingResolver> all = this.Components.GetAll<IBindingResolver>();
					all.SelectMany((IBindingResolver resolver) => resolver.Resolve(this._bindings, service)).Map(delegate(IBinding binding)
					{
						this._bindingCache.Add(service, binding);
					});
				}
				result = this._bindingCache[service];
			}
			finally
			{
				Monitor.Exit(bindingCache);
			}
			return result;
		}
		public virtual IActivationBlock BeginBlock()
		{
			return new ActivationBlock(this);
		}
		protected override BindingBuilder<T> CreateBindingBuilder<T>(IBinding binding)
		{
			return new BindingBuilder<T>(binding, this);
		}
		protected abstract void AddComponents();
		protected virtual bool HandleMissingBinding(Type service)
		{
			Ensure.ArgumentNotNull(service, "service");
			if (!this.TypeIsSelfBindable(service))
			{
				return false;
			}
			Binding binding = new Binding(service)
			{
				ProviderCallback = StandardProvider.GetCreationCallback(service),
				IsImplicit = true
			};
			this.AddBinding(binding);
			return true;
		}
		protected virtual bool TypeIsSelfBindable(Type service)
		{
			return !service.IsInterface && !service.IsAbstract && !service.IsValueType && service != typeof(string) && !service.ContainsGenericParameters;
		}
		protected virtual IContext CreateContext(IRequest request, IBinding binding)
		{
			Ensure.ArgumentNotNull(request, "request");
			Ensure.ArgumentNotNull(binding, "binding");
			return new Context(this, request, binding, this.Components.Get<ICache>(), this.Components.Get<IPlanner>(), this.Components.Get<IPipeline>());
		}
		object IServiceProvider.GetService(Type service)
		{
			return this.Get(service, new IParameter[0]);
		}
	}
}
