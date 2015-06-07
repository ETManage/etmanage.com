using Ninject.Infrastructure;
using System;
using System.Collections.Generic;
namespace Ninject.Planning.Bindings
{
	public class BindingMetadata : IBindingMetadata
	{
		private readonly Dictionary<string, object> _values = new Dictionary<string, object>();
		public string Name
		{
			get;
			set;
		}
		public bool Has(string key)
		{
			Ensure.ArgumentNotNullOrEmpty(key, "key");
			return this._values.ContainsKey(key);
		}
		public T Get<T>(string key)
		{
			Ensure.ArgumentNotNullOrEmpty(key, "key");
			return this.Get<T>(key, default(T));
		}
		public T Get<T>(string key, T defaultValue)
		{
			Ensure.ArgumentNotNullOrEmpty(key, "key");
			if (!this._values.ContainsKey(key))
			{
				return defaultValue;
			}
			return (T)((object)this._values[key]);
		}
		public void Set(string key, object value)
		{
			Ensure.ArgumentNotNullOrEmpty(key, "key");
			this._values[key] = value;
		}
	}
}
