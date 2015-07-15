using Ninject.Infrastructure;
using System;
namespace Ninject.Syntax
{
	public interface IBindingOnSyntax<T> : IBindingSyntax, IFluentSyntax, IHaveBinding, IHaveKernel
	{
		IBindingOnSyntax<T> OnActivation(Action<T> action);
		IBindingOnSyntax<T> OnDeactivation(Action<T> action);
	}
}
