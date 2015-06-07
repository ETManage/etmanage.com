using Ninject.Infrastructure;
using System;
namespace Ninject.Syntax
{
	public interface IBindingSyntax : IFluentSyntax, IHaveBinding, IHaveKernel
	{
	}
}
