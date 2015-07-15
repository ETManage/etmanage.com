using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Injection;
using Ninject.Planning.Directives;
using Ninject.Selection;
using System;
using System.Reflection;
namespace Ninject.Planning.Strategies
{
	public class MethodReflectionStrategy : NinjectComponent, IPlanningStrategy, INinjectComponent, IDisposable
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
		public MethodReflectionStrategy(ISelector selector, IInjectorFactory injectorFactory)
		{
			Ensure.ArgumentNotNull(selector, "selector");
			Ensure.ArgumentNotNull(injectorFactory, "injectorFactory");
			this.Selector = selector;
			this.InjectorFactory = injectorFactory;
		}
		public void Execute(IPlan plan)
		{
			Ensure.ArgumentNotNull(plan, "plan");
			foreach (MethodInfo current in this.Selector.SelectMethodsForInjection(plan.Type))
			{
				plan.Add(new MethodInjectionDirective(current, this.InjectorFactory.Create(current)));
			}
		}
	}
}
