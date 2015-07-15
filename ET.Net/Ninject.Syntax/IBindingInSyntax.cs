using Ninject.Activation;
using Ninject.Infrastructure;
using System;
namespace Ninject.Syntax
{
	public interface IBindingInSyntax<T> : IBindingSyntax, IFluentSyntax, IHaveBinding, IHaveKernel
	{
		IBindingNamedWithOrOnSyntax<T> InSingletonScope();
		IBindingNamedWithOrOnSyntax<T> InTransientScope();
		IBindingNamedWithOrOnSyntax<T> InThreadScope();
		IBindingNamedWithOrOnSyntax<T> InRequestScope();
		IBindingNamedWithOrOnSyntax<T> InScope(Func<IContext, object> scope);
	}
}
