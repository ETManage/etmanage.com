using Ninject.Injection;
using Ninject.Planning.Targets;
using System;
using System.Reflection;
namespace Ninject.Planning.Directives
{
	public class PropertyInjectionDirective : IDirective
	{
		public PropertyInjector Injector
		{
			get;
			private set;
		}
		public ITarget Target
		{
			get;
			private set;
		}
		public PropertyInjectionDirective(PropertyInfo member, PropertyInjector injector)
		{
			this.Injector = injector;
			this.Target = this.CreateTarget(member);
		}
		protected virtual ITarget CreateTarget(PropertyInfo propertyInfo)
		{
			return new PropertyTarget(propertyInfo);
		}
	}
}
