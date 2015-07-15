using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Injection;
using Ninject.Planning.Directives;
using Ninject.Selection;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Ninject.Planning.Strategies
{
	public class ConstructorReflectionStrategy : NinjectComponent, IPlanningStrategy, INinjectComponent, IDisposable
	{
		public ISelector Selector
		{
			get;
			private set;
		}
		public IInjectorFactory InjectorFactory
		{
			get;
			set;
		}
		public ConstructorReflectionStrategy(ISelector selector, IInjectorFactory injectorFactory)
		{
			Ensure.ArgumentNotNull(selector, "selector");
			Ensure.ArgumentNotNull(injectorFactory, "injectorFactory");
			this.Selector = selector;
			this.InjectorFactory = injectorFactory;
		}
		public void Execute(IPlan plan)
		{
			Ensure.ArgumentNotNull(plan, "plan");
			IEnumerable<ConstructorInfo> enumerable = this.Selector.SelectConstructorsForInjection(plan.Type);
			if (enumerable == null)
			{
				return;
			}
			foreach (ConstructorInfo current in enumerable)
			{
				plan.Add(new ConstructorInjectionDirective(current, this.InjectorFactory.Create(current)));
			}
		}
	}
}
