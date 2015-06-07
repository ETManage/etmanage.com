using Ninject.Infrastructure.Disposal;
using Ninject.Syntax;
using System;
namespace Ninject.Activation.Blocks
{
	public interface IActivationBlock : IResolutionRoot, INotifyWhenDisposed, IDisposableObject, IDisposable
	{
	}
}
