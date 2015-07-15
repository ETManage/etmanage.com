using Ninject.Infrastructure;
using System;
namespace Ninject.Activation.Providers
{
	public class CallbackProvider<T> : Provider<T>
	{
		public Func<IContext, T> Method
		{
			get;
			private set;
		}
		public CallbackProvider(Func<IContext, T> method)
		{
			Ensure.ArgumentNotNull(method, "method");
			this.Method = method;
		}
		protected override T CreateInstance(IContext context)
		{
			return this.Method(context);
		}
	}
}
