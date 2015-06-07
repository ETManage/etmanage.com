using Ninject.Infrastructure;
using Ninject.Infrastructure.Introspection;
using Ninject.Injection;
using Ninject.Parameters;
using Ninject.Planning.Directives;
using Ninject.Planning.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Ninject.Activation.Strategies
{
	public class PropertyInjectionStrategy : ActivationStrategy
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
		public IInjectorFactory InjectorFactory
		{
			get;
			set;
		}
		public PropertyInjectionStrategy(IInjectorFactory injectorFactory)
		{
			this.InjectorFactory = injectorFactory;
		}
		public override void Activate(IContext context, InstanceReference reference)
		{
			Ensure.ArgumentNotNull(context, "context");
			Ensure.ArgumentNotNull(reference, "reference");
			IEnumerable<IParameter> enumerable = 
				from parameter in context.Parameters
				where parameter is PropertyValue
				select parameter;
			IEnumerable<string> source = 
				from parameter in enumerable
				select parameter.Name;
			foreach (PropertyInjectionDirective current in context.Plan.GetAll<PropertyInjectionDirective>())
			{
				PropertyInjectionDirective propertyInjectionDirective = current;
				if (!source.Any((string name) => object.Equals(name, propertyInjectionDirective)))
				{
					object value = this.GetValue(context, current.Target);
					current.Injector(reference.Instance, value);
				}
			}
			this.AssignProperyOverrides(context, reference, enumerable);
		}
		private void AssignProperyOverrides(IContext context, InstanceReference reference, IEnumerable<IParameter> propertyValues)
		{
			PropertyInfo[] properties = reference.Instance.GetType().GetProperties(this.Flags);
			foreach (IParameter current in propertyValues)
			{
				string propertyName = current.Name;
				PropertyInfo propertyInfo = (
					from property in properties
					where string.Equals(property.Name, propertyName, StringComparison.Ordinal)
					select property).FirstOrDefault<PropertyInfo>();
				if (propertyInfo == null)
				{
					throw new ActivationException(ExceptionFormatter.CouldNotResolveProperyForValueInjection(context.Request, propertyName));
				}
				PropertyInjectionDirective propertyInjectionDirective = new PropertyInjectionDirective(propertyInfo, this.InjectorFactory.Create(propertyInfo));
				object value = this.GetValue(context, propertyInjectionDirective.Target);
				propertyInjectionDirective.Injector(reference.Instance, value);
			}
		}
		public object GetValue(IContext context, ITarget target)
		{
			Ensure.ArgumentNotNull(context, "context");
			Ensure.ArgumentNotNull(target, "target");
			PropertyValue propertyValue = (
				from p in context.Parameters.OfType<PropertyValue>()
				where p.Name == target.Name
				select p).SingleOrDefault<PropertyValue>();
			if (propertyValue == null)
			{
				return target.ResolveWithin(context);
			}
			return propertyValue.GetValue(context);
		}
	}
}
