using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Selection.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Ninject.Selection
{
	public class Selector : NinjectComponent, ISelector, INinjectComponent, IDisposable
	{
		private const BindingFlags DefaultFlags = BindingFlags.Instance | BindingFlags.Public;
		private BindingFlags Flags
		{
			get
			{
				if (!base.Settings.InjectNonPublic)
				{
					return BindingFlags.Instance | BindingFlags.Public;
				}
				return BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			}
		}
		public IConstructorScorer ConstructorScorer
		{
			get;
			set;
		}
		public ICollection<IInjectionHeuristic> InjectionHeuristics
		{
			get;
			private set;
		}
		public Selector(IConstructorScorer constructorScorer, IEnumerable<IInjectionHeuristic> injectionHeuristics)
		{
			Ensure.ArgumentNotNull(constructorScorer, "constructorScorer");
			Ensure.ArgumentNotNull(injectionHeuristics, "injectionHeuristics");
			this.ConstructorScorer = constructorScorer;
			this.InjectionHeuristics = injectionHeuristics.ToList<IInjectionHeuristic>();
		}
		public IEnumerable<ConstructorInfo> SelectConstructorsForInjection(Type type)
		{
			Ensure.ArgumentNotNull(type, "type");
			ConstructorInfo[] constructors = type.GetConstructors(this.Flags);
			if (constructors.Length != 0)
			{
				return constructors;
			}
			return null;
		}
		public IEnumerable<PropertyInfo> SelectPropertiesForInjection(Type type)
		{
			Ensure.ArgumentNotNull(type, "type");
			return 
				from p in type.GetProperties(this.Flags)
				where this.InjectionHeuristics.Any((IInjectionHeuristic h) => h.ShouldInject(p))
				select p;
		}
		public IEnumerable<MethodInfo> SelectMethodsForInjection(Type type)
		{
			Ensure.ArgumentNotNull(type, "type");
			return 
				from m in type.GetMethods(this.Flags)
				where this.InjectionHeuristics.Any((IInjectionHeuristic h) => h.ShouldInject(m))
				select m;
		}
	}
}
