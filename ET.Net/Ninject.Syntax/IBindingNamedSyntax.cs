using Ninject.Infrastructure;
using System;
namespace Ninject.Syntax
{
	public interface IBindingNamedSyntax<T> : IBindingSyntax, IFluentSyntax, IHaveBinding, IHaveKernel
	{
		IBindingWithSyntax<T> Named(string name);
	}
}
