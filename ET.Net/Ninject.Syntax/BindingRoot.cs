using Ninject.Infrastructure;
using Ninject.Infrastructure.Disposal;
using Ninject.Planning.Bindings;
using System;
namespace Ninject.Syntax
{
	public abstract class BindingRoot : DisposableObject, IBindingRoot
	{
		public IBindingToSyntax<T> Bind<T>()
		{
			Type typeFromHandle = typeof(T);
			Binding binding = new Binding(typeFromHandle);
			this.AddBinding(binding);
			return this.CreateBindingBuilder<T>(binding);
		}
		public IBindingToSyntax<object> Bind(Type service)
		{
			Ensure.ArgumentNotNull(service, "service");
			Binding binding = new Binding(service);
			this.AddBinding(binding);
			return this.CreateBindingBuilder<object>(binding);
		}
		public void Unbind<T>()
		{
			this.Unbind(typeof(T));
		}
		public abstract void Unbind(Type service);
		public IBindingToSyntax<T> Rebind<T>()
		{
			this.Unbind<T>();
			return this.Bind<T>();
		}
		public IBindingToSyntax<object> Rebind(Type service)
		{
			this.Unbind(service);
			return this.Bind(service);
		}
		public abstract void AddBinding(IBinding binding);
		public abstract void RemoveBinding(IBinding binding);
		protected abstract BindingBuilder<T> CreateBindingBuilder<T>(IBinding binding);
	}
}
