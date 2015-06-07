using Ninject.Infrastructure;
using Ninject.Infrastructure.Disposal;
using Ninject.Infrastructure.Introspection;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Ninject.Components
{
	public class ComponentContainer : DisposableObject, IComponentContainer, IDisposable
	{
		private readonly Multimap<Type, Type> _mappings = new Multimap<Type, Type>();
		private readonly Dictionary<Type, INinjectComponent> _instances = new Dictionary<Type, INinjectComponent>();
		public IKernel Kernel
		{
			get;
			set;
		}
		public override void Dispose(bool disposing)
		{
			if (disposing && !base.IsDisposed)
			{
				foreach (INinjectComponent current in this._instances.Values)
				{
					current.Dispose();
				}
				this._mappings.Clear();
				this._instances.Clear();
			}
			base.Dispose(disposing);
		}
		public void Add<TComponent, TImplementation>() where TComponent : INinjectComponent where TImplementation : TComponent, INinjectComponent
		{
			this._mappings.Add(typeof(TComponent), typeof(TImplementation));
		}
		public void RemoveAll<T>() where T : INinjectComponent
		{
			this.RemoveAll(typeof(T));
		}
		public void RemoveAll(Type component)
		{
			Ensure.ArgumentNotNull(component, "component");
			foreach (Type current in this._mappings[component])
			{
				if (this._instances.ContainsKey(current))
				{
					this._instances[current].Dispose();
				}
				this._instances.Remove(current);
			}
			this._mappings.RemoveAll(component);
		}
		public T Get<T>() where T : INinjectComponent
		{
			return (T)((object)this.Get(typeof(T)));
		}
		public IEnumerable<T> GetAll<T>() where T : INinjectComponent
		{
			return this.GetAll(typeof(T)).Cast<T>();
		}
		public object Get(Type component)
		{
			Ensure.ArgumentNotNull(component, "component");
			if (component == typeof(IKernel))
			{
				return this.Kernel;
			}
			if (component.IsGenericType)
			{
				Type genericTypeDefinition = component.GetGenericTypeDefinition();
				Type type = component.GetGenericArguments()[0];
				if (genericTypeDefinition.IsInterface && typeof(IEnumerable<>).IsAssignableFrom(genericTypeDefinition))
				{
					return this.GetAll(type).CastSlow(type);
				}
			}
			Type type2 = this._mappings[component].FirstOrDefault<Type>();
			if (type2 == null)
			{
				throw new InvalidOperationException(ExceptionFormatter.NoSuchComponentRegistered(component));
			}
			return this.ResolveInstance(component, type2);
		}
		public IEnumerable<object> GetAll(Type component)
		{
			Ensure.ArgumentNotNull(component, "component");
			foreach (Type current in this._mappings[component])
			{
				yield return this.ResolveInstance(component, current);
			}
			yield break;
		}
		private object ResolveInstance(Type component, Type implementation)
		{
			if (!this._instances.ContainsKey(implementation))
			{
				return this.CreateNewInstance(component, implementation);
			}
			return this._instances[implementation];
		}
		private object CreateNewInstance(Type component, Type implementation)
		{
			ConstructorInfo constructorInfo = ComponentContainer.SelectConstructor(component, implementation);
			object[] parameters = (
				from parameter in constructorInfo.GetParameters()
				select this.Get(parameter.ParameterType)).ToArray<object>();
			object result;
			try
			{
				INinjectComponent ninjectComponent = constructorInfo.Invoke(parameters) as INinjectComponent;
				ninjectComponent.Settings = this.Kernel.Settings;
				this._instances.Add(implementation, ninjectComponent);
				result = ninjectComponent;
			}
			catch (TargetInvocationException exception)
			{
				exception.RethrowInnerException();
				result = null;
			}
			return result;
		}
		private static ConstructorInfo SelectConstructor(Type component, Type implementation)
		{
			ConstructorInfo constructorInfo = (
				from c in implementation.GetConstructors()
				orderby c.GetParameters().Length descending
				select c).FirstOrDefault<ConstructorInfo>();
			if (constructorInfo == null)
			{
				throw new InvalidOperationException(ExceptionFormatter.NoConstructorsAvailableForComponent(component, implementation));
			}
			return constructorInfo;
		}
	}
}
