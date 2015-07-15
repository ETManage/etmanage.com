using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Injection;
using Ninject.Planning.Directives;
using Ninject.Selection;
using System;
using System.Reflection;
namespace Ninject.Planning.Strategies
{
	public class PropertyReflectionStrategy : NinjectComponent, IPlanningStrategy, INinjectComponent, IDisposable
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
		public PropertyReflectionStrategy(ISelector selector, IInjectorFactory injectorFactory)
		{
			Ensure.ArgumentNotNull(selector, "selector");
			Ensure.ArgumentNotNull(injectorFactory, "injectorFactory");
			this.Selector = selector;
			this.InjectorFactory = injectorFactory;
		}
		public void Execute(IPlan plan)
		{
			Ensure.ArgumentNotNull(plan, "plan");
			foreach (PropertyInfo current in this.Selector.SelectPropertiesForInjection(plan.Type))
			{
				plan.Add(new PropertyInjectionDirective(current, this.InjectorFactory.Create(current)));
			}
		}
	}
}
