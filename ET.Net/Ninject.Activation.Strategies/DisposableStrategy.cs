using System;
namespace Ninject.Activation.Strategies
{
	public class DisposableStrategy : ActivationStrategy
	{
		public override void Deactivate(IContext context, InstanceReference reference)
		{
			reference.IfInstanceIs<IDisposable>(delegate(IDisposable x)
			{
				x.Dispose();
			});
		}
	}
}
