using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using System;
namespace Ninject.Activation.Strategies
{
	public class BindingActionStrategy : ActivationStrategy
	{
		public override void Activate(IContext context, InstanceReference reference)
		{
			Ensure.ArgumentNotNull(context, "context");
			context.Binding.ActivationActions.Map(delegate(Action<object> action)
			{
				action(reference.Instance);
			});
		}
		public override void Deactivate(IContext context, InstanceReference reference)
		{
			Ensure.ArgumentNotNull(context, "context");
			context.Binding.DeactivationActions.Map(delegate(Action<object> action)
			{
				action(reference.Instance);
			});
		}
	}
}
