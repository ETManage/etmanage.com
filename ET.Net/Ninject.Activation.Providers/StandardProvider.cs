using Ninject.Infrastructure;
using Ninject.Infrastructure.Introspection;
using Ninject.Parameters;
using Ninject.Planning;
using Ninject.Planning.Directives;
using Ninject.Planning.Targets;
using Ninject.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Ninject.Activation.Providers
{
	public class StandardProvider : IProvider
	{
		public Type Type
		{
			get;
			private set;
		}
		public IPlanner Planner
		{
			get;
			private set;
		}
		public ISelector Selector
		{
			get;
			private set;
		}
		public StandardProvider(Type type, IPlanner planner, ISelector selector)
		{
			Ensure.ArgumentNotNull(type, "type");
			Ensure.ArgumentNotNull(planner, "planner");
			Ensure.ArgumentNotNull(selector, "selector");
			this.Type = type;
			this.Planner = planner;
			this.Selector = selector;
		}
		public virtual object Create(IContext context)
		{
			Ensure.ArgumentNotNull(context, "context");
			if (context.Plan == null)
			{
				context.Plan = this.Planner.GetPlan(this.GetImplementationType(context.Request.Service));
			}
			if (!context.Plan.Has<ConstructorInjectionDirective>())
			{
				throw new ActivationException(ExceptionFormatter.NoConstructorsAvailable(context));
			}
			IEnumerable<ConstructorInjectionDirective> all = context.Plan.GetAll<ConstructorInjectionDirective>();
			ConstructorInjectionDirective constructorInjectionDirective = (
				from option in all
				orderby this.Selector.ConstructorScorer.Score(context, option) descending
				select option).First<ConstructorInjectionDirective>();
			object[] arguments = (
				from target in constructorInjectionDirective.Targets
				select this.GetValue(context, target)).ToArray<object>();
			return constructorInjectionDirective.Injector(arguments);
		}
		public object GetValue(IContext context, ITarget target)
		{
			Ensure.ArgumentNotNull(context, "context");
			Ensure.ArgumentNotNull(target, "target");
			ConstructorArgument constructorArgument = (
				from p in context.Parameters.OfType<ConstructorArgument>()
				where p.Name == target.Name
				select p).SingleOrDefault<ConstructorArgument>();
			if (constructorArgument == null)
			{
				return target.ResolveWithin(context);
			}
			return constructorArgument.GetValue(context);
		}
		public Type GetImplementationType(Type service)
		{
			Ensure.ArgumentNotNull(service, "service");
			if (!this.Type.ContainsGenericParameters)
			{
				return this.Type;
			}
			return this.Type.MakeGenericType(service.GetGenericArguments());
		}
		public static Func<IContext, IProvider> GetCreationCallback(Type prototype)
		{
			Ensure.ArgumentNotNull(prototype, "prototype");
			return (IContext ctx) => new StandardProvider(prototype, ctx.Kernel.Components.Get<IPlanner>(), ctx.Kernel.Components.Get<ISelector>());
		}
	}
}
