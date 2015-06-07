using Ninject.Injection;
using System;
using System.Reflection;
namespace Ninject.Planning.Directives
{
	public class MethodInjectionDirective : MethodInjectionDirectiveBase<MethodInfo, MethodInjector>
	{
		public MethodInjectionDirective(MethodInfo method, MethodInjector injector) : base(method, injector)
		{
		}
	}
}
