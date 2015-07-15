using Ninject.Components;
using System;
using System.Reflection;
namespace Ninject.Injection
{
	public class ReflectionInjectorFactory : NinjectComponent, IInjectorFactory, INinjectComponent, IDisposable
	{
		public ConstructorInjector Create(ConstructorInfo constructor)
		{
			return (object[] args) => constructor.Invoke(args);
		}
		public PropertyInjector Create(PropertyInfo property)
		{
			return delegate(object target, object value)
			{
				property.SetValue(target, value, null);
			};
		}
		public MethodInjector Create(MethodInfo method)
		{
			return delegate(object target, object[] args)
			{
				method.Invoke(target, args);
			};
		}
	}
}
