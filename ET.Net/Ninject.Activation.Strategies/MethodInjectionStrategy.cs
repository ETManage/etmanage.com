using Ninject.Infrastructure;
using Ninject.Planning.Directives;
using Ninject.Planning.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Ninject.Activation.Strategies
{
	public class MethodInjectionStrategy : ActivationStrategy
	{
		public override void Activate(IContext context, InstanceReference reference)
		{
			Ensure.ArgumentNotNull(context, "context");
			Ensure.ArgumentNotNull(reference, "reference");
			foreach (MethodInjectionDirective current in context.Plan.GetAll<MethodInjectionDirective>())
			{
				IEnumerable<object> source = 
					from target in current.Targets
					select target.ResolveWithin(context);
				current.Injector(reference.Instance, source.ToArray<object>());
			}
		}
	}
}
