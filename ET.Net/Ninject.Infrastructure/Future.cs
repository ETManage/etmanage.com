using System;
namespace Ninject.Infrastructure
{
	public class Future<T>
	{
		private bool _hasValue;
		private T _value;
		public T Value
		{
			get
			{
				if (!this._hasValue)
				{
					this._value = this.Callback();
					this._hasValue = true;
				}
				return this._value;
			}
		}
		public Func<T> Callback
		{
			get;
			private set;
		}
		public Future(Func<T> callback)
		{
			this.Callback = callback;
		}
		public static implicit operator T(Future<T> future)
		{
			return future.Value;
		}
	}
}
