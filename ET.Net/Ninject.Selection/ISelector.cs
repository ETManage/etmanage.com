using Ninject.Components;
using Ninject.Selection.Heuristics;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Ninject.Selection
{
	public interface ISelector : INinjectComponent, IDisposable
	{
		IConstructorScorer ConstructorScorer
		{
			get;
			set;
		}
		ICollection<IInjectionHeuristic> InjectionHeuristics
		{
			get;
		}
		IEnumerable<ConstructorInfo> SelectConstructorsForInjection(Type type);
		IEnumerable<PropertyInfo> SelectPropertiesForInjection(Type type);
		IEnumerable<MethodInfo> SelectMethodsForInjection(Type type);
	}
}
