using System;
namespace Ninject.Activation
{
	public class InstanceReference
	{
		public object Instance
		{
			get;
			set;
		}
		public bool Is<T>()
		{
			return this.Instance is T;
		}
		public T As<T>()
		{
			return (T)((object)this.Instance);
		}
		public void IfInstanceIs<T>(Action<T> action)
		{
			if (this.Instance is T)
			{
				action((T)((object)this.Instance));
			}
		}
	}
}
