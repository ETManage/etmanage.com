using Ninject.Activation;
using Ninject.Activation.Caching;
using Ninject.Activation.Strategies;
using Ninject.Injection;
using Ninject.Modules;
using Ninject.Planning;
using Ninject.Planning.Bindings.Resolvers;
using Ninject.Planning.Strategies;
using Ninject.Selection;
using Ninject.Selection.Heuristics;
using System;
namespace Ninject
{
	public class StandardKernel : KernelBase
	{
		public StandardKernel(params INinjectModule[] modules) : base(modules)
		{
		}
		public StandardKernel(INinjectSettings settings, params INinjectModule[] modules) : base(settings, modules)
		{
		}
		protected override void AddComponents()
		{
			base.Components.Add<IPlanner, Planner>();
			base.Components.Add<IPlanningStrategy, ConstructorReflectionStrategy>();
			base.Components.Add<IPlanningStrategy, PropertyReflectionStrategy>();
			base.Components.Add<IPlanningStrategy, MethodReflectionStrategy>();
			base.Components.Add<ISelector, Selector>();
			base.Components.Add<IConstructorScorer, StandardConstructorScorer>();
			base.Components.Add<IInjectionHeuristic, StandardInjectionHeuristic>();
			base.Components.Add<IPipeline, Pipeline>();
			base.Components.Add<IActivationStrategy, PropertyInjectionStrategy>();
			base.Components.Add<IActivationStrategy, MethodInjectionStrategy>();
			base.Components.Add<IActivationStrategy, InitializableStrategy>();
			base.Components.Add<IActivationStrategy, StartableStrategy>();
			base.Components.Add<IActivationStrategy, BindingActionStrategy>();
			base.Components.Add<IActivationStrategy, DisposableStrategy>();
			base.Components.Add<IBindingResolver, StandardBindingResolver>();
			base.Components.Add<IBindingResolver, OpenGenericBindingResolver>();
			if (!base.Settings.UseReflectionBasedInjection)
			{
				base.Components.Add<IInjectorFactory, DynamicMethodInjectorFactory>();
			}
			else
			{
				base.Components.Add<IInjectorFactory, ReflectionInjectorFactory>();
			}
			base.Components.Add<ICache, Cache>();
			base.Components.Add<ICachePruner, GarbageCollectionCachePruner>();
			base.Components.Add<IModuleLoader, ModuleLoader>();
			base.Components.Add<IModuleLoaderPlugin, CompiledModuleLoaderPlugin>();
		}
	}
}
