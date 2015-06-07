using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using Ninject.Planning.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Ninject.Planning
{
	public class Planner : NinjectComponent, IPlanner, INinjectComponent, IDisposable
	{
		private readonly Dictionary<Type, IPlan> _plans = new Dictionary<Type, IPlan>();
		public IList<IPlanningStrategy> Strategies
		{
			get;
			private set;
		}
		public Planner(IEnumerable<IPlanningStrategy> strategies)
		{
			Ensure.ArgumentNotNull(strategies, "strategies");
			this.Strategies = strategies.ToList<IPlanningStrategy>();
		}
		public IPlan GetPlan(Type type)
		{
			Ensure.ArgumentNotNull(type, "type");
			if (this._plans.ContainsKey(type))
			{
				return this._plans[type];
			}
			IPlan plan = this.CreateEmptyPlan(type);
			this._plans.Add(type, plan);
			this.Strategies.Map(delegate(IPlanningStrategy s)
			{
				s.Execute(plan);
			});
			return plan;
		}
		protected virtual IPlan CreateEmptyPlan(Type type)
		{
			Ensure.ArgumentNotNull(type, "type");
			return new Plan(type);
		}
	}
}
