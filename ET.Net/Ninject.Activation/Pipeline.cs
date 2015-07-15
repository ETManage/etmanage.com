using Ninject.Activation.Strategies;
using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Ninject.Activation
{
	public class Pipeline : NinjectComponent, IPipeline, INinjectComponent, IDisposable
	{
		public IList<IActivationStrategy> Strategies
		{
			get;
			private set;
		}
		public Pipeline(IEnumerable<IActivationStrategy> strategies)
		{
			Ensure.ArgumentNotNull(strategies, "strategies");
			this.Strategies = strategies.ToList<IActivationStrategy>();
		}
		public void Activate(IContext context, InstanceReference reference)
		{
			Ensure.ArgumentNotNull(context, "context");
			this.Strategies.Map(delegate(IActivationStrategy s)
			{
				s.Activate(context, reference);
			});
		}
		public void Deactivate(IContext context, InstanceReference reference)
		{
			Ensure.ArgumentNotNull(context, "context");
			this.Strategies.Map(delegate(IActivationStrategy s)
			{
				s.Deactivate(context, reference);
			});
		}
	}
}
