using Ninject.Components;
using System;
using System.Reflection;
namespace Ninject.Selection.Heuristics
{
	public interface IInjectionHeuristic : INinjectComponent, IDisposable
	{
		bool ShouldInject(MemberInfo member);
	}
}
