using Ninject.Activation;
using Ninject.Infrastructure;
using System;
namespace Ninject.Syntax
{
	public interface IBindingToSyntax<T> : IBindingSyntax, IFluentSyntax, IHaveBinding, IHaveKernel
	{
		IBindingWhenInNamedWithOrOnSyntax<T> ToSelf();
		IBindingWhenInNamedWithOrOnSyntax<T> To<TImplementation>() where TImplementation : T;
		IBindingWhenInNamedWithOrOnSyntax<T> To(Type implementation);
		IBindingWhenInNamedWithOrOnSyntax<T> ToProvider<TProvider>() where TProvider : IProvider;
		IBindingWhenInNamedWithOrOnSyntax<T> ToProvider(Type providerType);
		IBindingWhenInNamedWithOrOnSyntax<T> ToProvider(IProvider provider);
		IBindingWhenInNamedWithOrOnSyntax<T> ToMethod(Func<IContext, T> method);
		IBindingWhenInNamedWithOrOnSyntax<T> ToConstant(T value);
	}
}
