using Ninject.Activation;
using System;
using System.Threading;
using System.Web;
namespace Ninject.Infrastructure
{
	public class StandardScopeCallbacks
	{
		public static readonly Func<IContext, object> Transient = (IContext ctx) => null;
		public static readonly Func<IContext, object> Singleton = (IContext ctx) => ctx.Kernel;
		public static readonly Func<IContext, object> Thread = (IContext ctx) => System.Threading.Thread.CurrentThread;
		public static readonly Func<IContext, object> Request = (IContext ctx) => HttpContext.Current;
	}
}
