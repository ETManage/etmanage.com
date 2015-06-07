using Ninject.Infrastructure;
using System;
namespace Ninject.Syntax
{
	public interface IBindingWithOrOnSyntax<T> : IBindingWithSyntax<T>, IBindingOnSyntax<T>, IBindingSyntax, IFluentSyntax, IHaveBinding, IHaveKernel
	{
	}
}
