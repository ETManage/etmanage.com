using Ninject.Components;
using Ninject.Planning.Strategies;
using System;
using System.Collections.Generic;
namespace Ninject.Planning
{
	public interface IPlanner : INinjectComponent, IDisposable
	{
		IList<IPlanningStrategy> Strategies
		{
			get;
		}
		IPlan GetPlan(Type type);
	}
}
