using Ninject.Components;
using System;
namespace Ninject.Activation.Strategies
{
	public abstract class ActivationStrategy : NinjectComponent, IActivationStrategy, INinjectComponent, IDisposable
	{
		public virtual void Activate(IContext context, InstanceReference reference)
		{
		}
		public virtual void Deactivate(IContext context, InstanceReference reference)
		{
		}
	}
}
