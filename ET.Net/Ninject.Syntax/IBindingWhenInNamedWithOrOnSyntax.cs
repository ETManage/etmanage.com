using Ninject.Infrastructure;
using System;
namespace Ninject.Syntax
{
	public interface IBindingWhenInNamedWithOrOnSyntax<T> : IBindingWhenSyntax<T>, IBindingInSyntax<T>, IBindingNamedSyntax<T>, IBindingWithSyntax<T>, IBindingOnSyntax<T>, IBindingSyntax, IFluentSyntax, IHaveBinding, IHaveKernel
	{
	}
}
