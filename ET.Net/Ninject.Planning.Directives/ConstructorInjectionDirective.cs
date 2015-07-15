using Ninject.Injection;
using System;
using System.Reflection;
namespace Ninject.Planning.Directives
{
	public class ConstructorInjectionDirective : MethodInjectionDirectiveBase<ConstructorInfo, ConstructorInjector>
	{
		public ConstructorInfo Constructor
		{
			get;
			set;
		}
		public ConstructorInjectionDirective(ConstructorInfo constructor, ConstructorInjector injector) : base(constructor, injector)
		{
			this.Constructor = constructor;
		}
	}
}
