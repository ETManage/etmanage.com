using Ninject.Infrastructure;
using Ninject.Infrastructure.Disposal;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
namespace Ninject.Activation.Blocks
{
	public class ActivationBlock : DisposableObject, IActivationBlock, IResolutionRoot, INotifyWhenDisposed, IDisposableObject, IDisposable
	{
		public event EventHandler Disposed
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.Disposed += (EventHandler)Delegate.Combine(this.Disposed, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
                this.Disposed += (EventHandler)Delegate.Remove(this.Disposed, value);
			}
		}
		public IResolutionRoot Parent
		{
			get;
			private set;
		}
		public ActivationBlock(IResolutionRoot parent)
		{
			Ensure.ArgumentNotNull(parent, "parent");
			this.Parent = parent;
		}
		public override void Dispose(bool disposing)
		{
			Monitor.Enter(this);
			try
			{
				if (disposing && !base.IsDisposed)
				{
					EventHandler disposed = this.Disposed;
					if (disposed != null)
					{
						disposed(this, EventArgs.Empty);
					}
					this.Disposed = null;
				}
				base.Dispose(disposing);
			}
			finally
			{
				Monitor.Exit(this);
			}
		}
		public bool CanResolve(IRequest request)
		{
			Ensure.ArgumentNotNull(request, "request");
			return this.Parent.CanResolve(request);
		}
		public IEnumerable<object> Resolve(IRequest request)
		{
			Ensure.ArgumentNotNull(request, "request");
			return this.Parent.Resolve(request);
		}
		public virtual IRequest CreateRequest(Type service, Func<IBindingMetadata, bool> constraint, IEnumerable<IParameter> parameters, bool isOptional, bool isUnique)
		{
			Ensure.ArgumentNotNull(service, "service");
			Ensure.ArgumentNotNull(parameters, "parameters");
			return new Request(service, constraint, parameters, () => this, isOptional, isUnique);
		}
	}
}
