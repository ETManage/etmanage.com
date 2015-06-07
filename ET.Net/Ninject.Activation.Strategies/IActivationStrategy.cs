using Ninject.Components;
using System;
namespace Ninject.Activation.Strategies
{
	public interface IActivationStrategy : INinjectComponent, IDisposable
	{
		void Activate(IContext context, InstanceReference reference);
		void Deactivate(IContext context, InstanceReference reference);
	}
}
