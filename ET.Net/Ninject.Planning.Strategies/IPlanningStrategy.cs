using Ninject.Components;
using System;
namespace Ninject.Planning.Strategies
{
	public interface IPlanningStrategy : INinjectComponent, IDisposable
	{
		void Execute(IPlan plan);
	}
}
