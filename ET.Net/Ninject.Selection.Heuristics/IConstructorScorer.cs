using Ninject.Activation;
using Ninject.Components;
using Ninject.Planning.Directives;
using System;
namespace Ninject.Selection.Heuristics
{
	public interface IConstructorScorer : INinjectComponent, IDisposable
	{
		int Score(IContext context, ConstructorInjectionDirective directive);
	}
}
