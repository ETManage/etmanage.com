using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Disposal;
using Ninject.Infrastructure.Language;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace Ninject.Activation.Caching
{
	public class Cache : NinjectComponent, ICache, INinjectComponent, IDisposable
	{
		private class CacheEntry
		{
			public IContext Context
			{
				get;
				private set;
			}
			public InstanceReference Reference
			{
				get;
				private set;
			}
			public WeakReference Scope
			{
				get;
				private set;
			}
			public CacheEntry(IContext context, InstanceReference reference)
			{
				this.Context = context;
				this.Reference = reference;
				this.Scope = new WeakReference(context.GetScope());
			}
		}
		private readonly Multimap<IBinding, Cache.CacheEntry> _entries = new Multimap<IBinding, Cache.CacheEntry>();
		public IPipeline Pipeline
		{
			get;
			private set;
		}
		public int Count
		{
			get
			{
				return this._entries.Sum((KeyValuePair<IBinding, ICollection<Cache.CacheEntry>> list) => list.Value.Count);
			}
		}
		public Cache(IPipeline pipeline, ICachePruner cachePruner)
		{
			Ensure.ArgumentNotNull(pipeline, "pipeline");
			Ensure.ArgumentNotNull(cachePruner, "cachePruner");
			this._entries = new Multimap<IBinding, Cache.CacheEntry>();
			this.Pipeline = pipeline;
			cachePruner.Start(this);
		}
		public override void Dispose(bool disposing)
		{
			if (disposing && !base.IsDisposed)
			{
				this.Clear();
			}
			base.Dispose(disposing);
		}
		public void Remember(IContext context, InstanceReference reference)
		{
			Ensure.ArgumentNotNull(context, "context");
			Cache.CacheEntry entry = new Cache.CacheEntry(context, reference);
			Multimap<IBinding, Cache.CacheEntry> entries;
			Monitor.Enter(entries = this._entries);
			try
			{
				this._entries[context.Binding].Add(entry);
			}
			finally
			{
				Monitor.Exit(entries);
			}
			INotifyWhenDisposed notifyWhenDisposed = context.GetScope() as INotifyWhenDisposed;
			if (notifyWhenDisposed != null)
			{
				notifyWhenDisposed.Disposed += delegate(object o, EventArgs e)
				{
					this.Forget(entry);
				};
			}
		}
		public object TryGet(IContext context)
		{
			Ensure.ArgumentNotNull(context, "context");
			Multimap<IBinding, Cache.CacheEntry> entries;
			Monitor.Enter(entries = this._entries);
			object result;
			try
			{
				object scope = context.GetScope();
				foreach (Cache.CacheEntry current in this._entries[context.Binding])
				{
					if (object.ReferenceEquals(current.Scope.Target, scope))
					{
						if (context.HasInferredGenericArguments)
						{
							Type[] genericArguments = current.Context.GenericArguments;
							Type[] genericArguments2 = context.GenericArguments;
							if (!genericArguments.SequenceEqual(genericArguments2))
							{
								continue;
							}
						}
						result = current.Reference.Instance;
						return result;
					}
				}
				result = null;
			}
			finally
			{
				Monitor.Exit(entries);
			}
			return result;
		}
		public bool Release(object instance)
		{
			return this.ForgetAllWhere((Cache.CacheEntry e) => object.ReferenceEquals(instance, e.Reference.Instance));
		}
		public void Prune()
		{
			this.ForgetAllWhere((Cache.CacheEntry e) => !e.Scope.IsAlive);
		}
		public void Clear(object scope)
		{
			this.ForgetAllWhere((Cache.CacheEntry e) => object.ReferenceEquals(scope, e.Scope.Target));
		}
		public void Clear()
		{
			this.ForgetAllWhere((Cache.CacheEntry e) => true);
		}
		private bool ForgetAllWhere(Func<Cache.CacheEntry, bool> predicate)
		{
			Multimap<IBinding, Cache.CacheEntry> entries;
			Monitor.Enter(entries = this._entries);
			bool result;
			try
			{
				Cache.CacheEntry[] array = this._entries.SelectMany((KeyValuePair<IBinding, ICollection<Cache.CacheEntry>> e) => e.Value).Where(predicate).ToArray<Cache.CacheEntry>();
				array.Map(new Action<Cache.CacheEntry>(this.Forget));
				result = array.Any<Cache.CacheEntry>();
			}
			finally
			{
				Monitor.Exit(entries);
			}
			return result;
		}
		private void Forget(Cache.CacheEntry entry)
		{
			this.Pipeline.Deactivate(entry.Context, entry.Reference);
			this._entries[entry.Context.Binding].Remove(entry);
		}
	}
}
