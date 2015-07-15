using Ninject.Planning.Bindings;
using System;
namespace Ninject.Syntax
{
	public interface IBindingRoot
	{
		IBindingToSyntax<T> Bind<T>();
		IBindingToSyntax<object> Bind(Type service);
		void Unbind<T>();
		void Unbind(Type service);
		IBindingToSyntax<T> Rebind<T>();
		IBindingToSyntax<object> Rebind(Type service);
		void AddBinding(IBinding binding);
		void RemoveBinding(IBinding binding);
	}
}
