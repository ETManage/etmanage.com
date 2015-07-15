using Ninject.Infrastructure;
using System;
namespace Ninject.Syntax
{
	public interface IBindingInNamedWithOrOnSyntax<T> : IBindingInSyntax<T>, IBindingNamedSyntax<T>, IBindingWithSyntax<T>, IBindingOnSyntax<T>, IBindingSyntax, IFluentSyntax, IHaveBinding, IHaveKernel
	{
	}
}
