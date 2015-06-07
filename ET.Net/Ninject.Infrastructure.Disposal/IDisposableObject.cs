using System;
namespace Ninject.Infrastructure.Disposal
{
	public interface IDisposableObject : IDisposable
	{
		bool IsDisposed
		{
			get;
		}
	}
}
