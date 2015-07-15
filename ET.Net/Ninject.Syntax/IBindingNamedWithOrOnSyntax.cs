using Ninject.Infrastructure;
using System;
namespace Ninject.Syntax
{
	public interface IBindingNamedWithOrOnSyntax<T> : IBindingNamedSyntax<T>, IBindingWithSyntax<T>, IBindingOnSyntax<T>, IBindingSyntax, IFluentSyntax, IHaveBinding, IHaveKernel
	{
	}
}
