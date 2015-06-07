using System;
namespace Ninject.Infrastructure.Disposal
{
	public interface INotifyWhenDisposed : IDisposableObject, IDisposable
	{
		event EventHandler Disposed;
	}
}
