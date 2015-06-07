using Ninject.Activation.Caching;
using Ninject.Infrastructure.Disposal;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Ninject
{
	public class OnePerRequestModule : DisposableObject, IHttpModule
	{
		private static readonly List<IKernel> Kernels = new List<IKernel>();
		public void Init(HttpApplication application)
		{
			application.EndRequest += delegate(object o, EventArgs e)
			{
				OnePerRequestModule.DeactivateInstancesForCurrentHttpRequest();
			};
		}
		public static void StartManaging(IKernel kernel)
		{
			OnePerRequestModule.Kernels.Add(kernel);
		}
		public static void StopManaging(IKernel kernel)
		{
			OnePerRequestModule.Kernels.Remove(kernel);
		}
		public static void DeactivateInstancesForCurrentHttpRequest()
		{
			HttpContext context = HttpContext.Current;
			(
				from kernel in OnePerRequestModule.Kernels
				select kernel.Components.Get<ICache>()).Map(delegate(ICache cache)
			{
				cache.Clear(context);
			});
		}
	}
}
