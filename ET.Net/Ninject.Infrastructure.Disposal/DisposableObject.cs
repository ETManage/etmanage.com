using System;
using System.Threading;
namespace Ninject.Infrastructure.Disposal
{
	public abstract class DisposableObject : IDisposableObject, IDisposable
	{
		public bool IsDisposed
		{
			get;
			private set;
		}
		public void Dispose()
		{
			this.Dispose(true);
		}
		public virtual void Dispose(bool disposing)
		{
			Monitor.Enter(this);
			try
			{
				if (disposing && !this.IsDisposed)
				{
					this.IsDisposed = true;
					GC.SuppressFinalize(this);
				}
			}
			finally
			{
				Monitor.Exit(this);
			}
		}
		~DisposableObject()
		{
			this.Dispose(false);
		}
	}
}
