using System;
namespace Ninject.Activation.Strategies
{
	public class StartableStrategy : ActivationStrategy
	{
		public override void Activate(IContext context, InstanceReference reference)
		{
			reference.IfInstanceIs<IStartable>(delegate(IStartable x)
			{
				x.Start();
			});
		}
		public override void Deactivate(IContext context, InstanceReference reference)
		{
			reference.IfInstanceIs<IStartable>(delegate(IStartable x)
			{
				x.Stop();
			});
		}
	}
}
