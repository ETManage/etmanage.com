using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
namespace Ninject.Modules
{
	public abstract class NinjectModule : BindingRoot, INinjectModule, IHaveKernel
	{
		public IKernel Kernel
		{
			get;
			protected set;
		}
		public virtual string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}
		public ICollection<IBinding> Bindings
		{
			get;
			private set;
		}
		protected NinjectModule()
		{
			this.Bindings = new List<IBinding>();
		}
		public void OnLoad(IKernel kernel)
		{
			Ensure.ArgumentNotNull(kernel, "kernel");
			this.Kernel = kernel;
			this.Load();
		}
		public void OnUnload(IKernel kernel)
		{
			Ensure.ArgumentNotNull(kernel, "kernel");
			this.Unload();
			this.Bindings.Map(new Action<IBinding>(this.Kernel.RemoveBinding));
			this.Kernel = null;
		}
		public abstract void Load();
		public virtual void Unload()
		{
		}
		public override void Unbind(Type service)
		{
			this.Kernel.Unbind(service);
		}
		public override void AddBinding(IBinding binding)
		{
			Ensure.ArgumentNotNull(binding, "binding");
			this.Kernel.AddBinding(binding);
			this.Bindings.Add(binding);
		}
		public override void RemoveBinding(IBinding binding)
		{
			Ensure.ArgumentNotNull(binding, "binding");
			this.Kernel.RemoveBinding(binding);
			this.Bindings.Remove(binding);
		}
		protected override BindingBuilder<T> CreateBindingBuilder<T>(IBinding binding)
		{
			return new BindingBuilder<T>(binding, this.Kernel);
		}
	}
}
