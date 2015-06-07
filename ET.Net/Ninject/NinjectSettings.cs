using System;
using System.Collections.Generic;
namespace Ninject
{
	public class NinjectSettings : INinjectSettings
	{
		private readonly Dictionary<string, object> _values = new Dictionary<string, object>();
		public Type InjectAttribute
		{
			get
			{
				return this.Get<Type>("InjectAttribute", typeof(InjectAttribute));
			}
			set
			{
				this.Set("InjectAttribute", value);
			}
		}
		public TimeSpan CachePruningInterval
		{
			get
			{
				return this.Get<TimeSpan>("CachePruningInterval", TimeSpan.FromSeconds(30.0));
			}
			set
			{
				this.Set("CachePruningInterval", value);
			}
		}
		public bool LoadExtensions
		{
			get
			{
				return this.Get<bool>("LoadExtensions", true);
			}
			set
			{
				this.Set("LoadExtensions", value);
			}
		}
		public string ExtensionSearchPattern
		{
			get
			{
				return this.Get<string>("ExtensionSearchPattern", "Ninject.Extensions.*.dll");
			}
			set
			{
				this.Set("ExtensionSearchPattern", value);
			}
		}
		public bool UseReflectionBasedInjection
		{
			get
			{
				return this.Get<bool>("UseReflectionBasedInjection", false);
			}
			set
			{
				this.Set("UseReflectionBasedInjection", value);
			}
		}
		public bool InjectNonPublic
		{
			get
			{
				return this.Get<bool>("InjectNonPublic", false);
			}
			set
			{
				this.Set("InjectNonPublic", value);
			}
		}
		public T Get<T>(string key, T defaultValue)
		{
			if (!this._values.ContainsKey(key))
			{
				return defaultValue;
			}
			return (T)((object)this._values[key]);
		}
		public void Set(string key, object value)
		{
			this._values[key] = value;
		}
	}
}
