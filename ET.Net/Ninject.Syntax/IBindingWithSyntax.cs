using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Parameters;
using System;
namespace Ninject.Syntax
{
	public interface IBindingWithSyntax<T> : IBindingSyntax, IFluentSyntax, IHaveBinding, IHaveKernel
	{
		IBindingWithOrOnSyntax<T> WithConstructorArgument(string name, object value);
		IBindingWithOrOnSyntax<T> WithConstructorArgument(string name, Func<IContext, object> callback);
		IBindingWithOrOnSyntax<T> WithPropertyValue(string name, object value);
		IBindingWithOrOnSyntax<T> WithPropertyValue(string name, Func<IContext, object> callback);
		IBindingWithOrOnSyntax<T> WithParameter(IParameter parameter);
		IBindingWithOrOnSyntax<T> WithMetadata(string key, object value);
	}
}
