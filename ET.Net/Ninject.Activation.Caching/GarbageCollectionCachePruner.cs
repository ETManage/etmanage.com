using Ninject.Components;
using Ninject.Infrastructure;
using System;
using System.Threading;
namespace Ninject.Activation.Caching
{
	public class GarbageCollectionCachePruner : NinjectComponent, ICachePruner, INinjectComponent, IDisposable
	{
		private readonly WeakReference _indicator = new WeakReference(new object());
		private Timer _timer;
		public ICache Cache
		{
			get;
			private set;
		}
		public override void Dispose(bool disposing)
		{
			if (disposing && !base.IsDisposed && this._timer != null)
			{
				this.Stop();
			}
			base.Dispose(disposing);
		}
		public void Start(ICache cache)
		{
			Ensure.ArgumentNotNull(cache, "cache");
			if (this._timer != null)
			{
				this.Stop();
			}
			this.Cache = cache;
			this._timer = new Timer(new TimerCallback(this.PruneCacheIfGarbageCollectorHasRun), null, this.GetTimeoutInMilliseconds(), -1);
		}
		public void Stop()
		{
			this._timer.Change(-1, -1);
			this._timer.Dispose();
			this._timer = null;
			this.Cache = null;
		}
		private void PruneCacheIfGarbageCollectorHasRun(object state)
		{
			if (this._indicator.IsAlive)
			{
				return;
			}
			this.Cache.Prune();
			this._indicator.Target = new object();
			this._timer.Change(this.GetTimeoutInMilliseconds(), -1);
		}
		private int GetTimeoutInMilliseconds()
		{
			TimeSpan cachePruningInterval = base.Settings.CachePruningInterval;
			if (!(cachePruningInterval == TimeSpan.MaxValue))
			{
				return (int)cachePruningInterval.TotalMilliseconds;
			}
			return -1;
		}
	}
}
