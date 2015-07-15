using System;
using System.Collections;
using System.Collections.Generic;
namespace Ninject.Infrastructure
{
	public class Multimap<K, V> : IEnumerable<KeyValuePair<K, ICollection<V>>>, IEnumerable
	{
		private readonly Dictionary<K, ICollection<V>> _items = new Dictionary<K, ICollection<V>>();
		public ICollection<V>this[K key]
		{
			get
			{
				Ensure.ArgumentNotNull(key, "key");
				if (!this._items.ContainsKey(key))
				{
					this._items[key] = new List<V>();
				}
				return this._items[key];
			}
		}
		public ICollection<K> Keys
		{
			get
			{
				return this._items.Keys;
			}
		}
		public ICollection<ICollection<V>> Values
		{
			get
			{
				return this._items.Values;
			}
		}
		public void Add(K key, V value)
		{
			Ensure.ArgumentNotNull(key, "key");
			Ensure.ArgumentNotNull(value, "value");
			this[key].Add(value);
		}
		public bool Remove(K key, V value)
		{
			Ensure.ArgumentNotNull(key, "key");
			Ensure.ArgumentNotNull(value, "value");
			return this._items.ContainsKey(key) && this._items[key].Remove(value);
		}
		public bool RemoveAll(K key)
		{
			Ensure.ArgumentNotNull(key, "key");
			return this._items.Remove(key);
		}
		public void Clear()
		{
			this._items.Clear();
		}
		public bool ContainsKey(K key)
		{
			Ensure.ArgumentNotNull(key, "key");
			return this._items.ContainsKey(key);
		}
		public bool ContainsValue(K key, V value)
		{
			Ensure.ArgumentNotNull(key, "key");
			Ensure.ArgumentNotNull(value, "value");
			return this._items.ContainsKey(key) && this._items[key].Contains(value);
		}
		public IEnumerator GetEnumerator()
		{
			foreach (KeyValuePair<K, ICollection<V>> current in this._items)
			{
				yield return current;
			}
			yield break;
		}
		IEnumerator<KeyValuePair<K, ICollection<V>>> IEnumerable<KeyValuePair<K, ICollection<V>>>.GetEnumerator()
		{
			foreach (KeyValuePair<K, ICollection<V>> current in this._items)
			{
				yield return current;
			}
			yield break;
		}
	}
}
