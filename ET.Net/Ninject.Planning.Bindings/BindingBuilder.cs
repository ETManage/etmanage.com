using Ninject.Activation;
using Ninject.Activation.Providers;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Introspection;
using Ninject.Infrastructure.Language;
using Ninject.Parameters;
using Ninject.Syntax;
using System;
namespace Ninject.Planning.Bindings
{
	public class BindingBuilder<T> : IBindingToSyntax<T>, IBindingWhenInNamedWithOrOnSyntax<T>, IBindingWhenSyntax<T>, IBindingInNamedWithOrOnSyntax<T>, IBindingInSyntax<T>, IBindingNamedWithOrOnSyntax<T>, IBindingNamedSyntax<T>, IBindingWithOrOnSyntax<T>, IBindingWithSyntax<T>, IBindingOnSyntax<T>, IBindingSyntax, IFluentSyntax, IHaveBinding, IHaveKernel
	{
		public IBinding Binding
		{
			get;
			private set;
		}
		public IKernel Kernel
		{
			get;
			private set;
		}
		public BindingBuilder(IBinding binding, IKernel kernel)
		{
			Ensure.ArgumentNotNull(binding, "binding");
			Ensure.ArgumentNotNull(kernel, "kernel");
			this.Binding = binding;
			this.Kernel = kernel;
		}
		public IBindingWhenInNamedWithOrOnSyntax<T> ToSelf()
		{
			this.Binding.ProviderCallback = StandardProvider.GetCreationCallback(this.Binding.Service);
			this.Binding.Target = BindingTarget.Self;
			return this;
		}
		public IBindingWhenInNamedWithOrOnSyntax<T> To<TImplementation>() where TImplementation : T
		{
			this.Binding.ProviderCallback = StandardProvider.GetCreationCallback(typeof(TImplementation));
			this.Binding.Target = BindingTarget.Type;
			return this;
		}
		public IBindingWhenInNamedWithOrOnSyntax<T> To(Type implementation)
		{
			this.Binding.ProviderCallback = StandardProvider.GetCreationCallback(implementation);
			this.Binding.Target = BindingTarget.Type;
			return this;
		}
		public IBindingWhenInNamedWithOrOnSyntax<T> ToProvider<TProvider>() where TProvider : IProvider
		{
            this.Binding.ProviderCallback = ((IContext ctx) => ctx.Kernel.Get<TProvider>(new IParameter[0]));
			this.Binding.Target = BindingTarget.Provider;
			return this;
		}
		public IBindingWhenInNamedWithOrOnSyntax<T> ToProvider(Type providerType)
		{
			this.Binding.ProviderCallback = ((IContext ctx) => ctx.Kernel.Get(providerType, new IParameter[0]) as IProvider);
			this.Binding.Target = BindingTarget.Provider;
			return this;
		}
		public IBindingWhenInNamedWithOrOnSyntax<T> ToProvider(IProvider provider)
		{
			this.Binding.ProviderCallback = ((IContext ctx) => provider);
			this.Binding.Target = BindingTarget.Provider;
			return this;
		}
		public IBindingWhenInNamedWithOrOnSyntax<T> ToMethod(Func<IContext, T> method)
		{
			this.Binding.ProviderCallback = ((IContext ctx) => new CallbackProvider<T>(method));
			this.Binding.Target = BindingTarget.Method;
			return this;
		}
		public IBindingWhenInNamedWithOrOnSyntax<T> ToConstant(T value)
		{
			this.Binding.ProviderCallback = ((IContext ctx) => new ConstantProvider<T>(value));
			this.Binding.Target = BindingTarget.Constant;
			return this;
		}
		public IBindingInNamedWithOrOnSyntax<T> When(Func<IRequest, bool> condition)
		{
			this.Binding.Condition = condition;
			return this;
		}
		public IBindingInNamedWithOrOnSyntax<T> WhenInjectedInto<TParent>()
		{
			return this.WhenInjectedInto(typeof(TParent));
		}
		public IBindingInNamedWithOrOnSyntax<T> WhenInjectedInto(Type parent)
		{
			this.Binding.Condition = ((IRequest r) => r.Target != null && r.Target.Member.ReflectedType == parent);
			return this;
		}
		public IBindingInNamedWithOrOnSyntax<T> WhenClassHas<TAttribute>() where TAttribute : Attribute
		{
			return this.WhenClassHas(typeof(TAttribute));
		}
		public IBindingInNamedWithOrOnSyntax<T> WhenMemberHas<TAttribute>() where TAttribute : Attribute
		{
			return this.WhenMemberHas(typeof(TAttribute));
		}
		public IBindingInNamedWithOrOnSyntax<T> WhenTargetHas<TAttribute>() where TAttribute : Attribute
		{
			return this.WhenTargetHas(typeof(TAttribute));
		}
		public IBindingInNamedWithOrOnSyntax<T> WhenClassHas(Type attributeType)
		{
			if (!typeof(Attribute).IsAssignableFrom(attributeType))
			{
				throw new InvalidOperationException(ExceptionFormatter.InvalidAttributeTypeUsedInBindingCondition(this.Binding, "WhenClassHas", attributeType));
			}
			this.Binding.Condition = ((IRequest r) => r.Target != null && r.Target.Member.ReflectedType.HasAttribute(attributeType));
			return this;
		}
		public IBindingInNamedWithOrOnSyntax<T> WhenMemberHas(Type attributeType)
		{
			if (!typeof(Attribute).IsAssignableFrom(attributeType))
			{
				throw new InvalidOperationException(ExceptionFormatter.InvalidAttributeTypeUsedInBindingCondition(this.Binding, "WhenMemberHas", attributeType));
			}
			this.Binding.Condition = ((IRequest r) => r.Target != null && r.Target.Member.HasAttribute(attributeType));
			return this;
		}
		public IBindingInNamedWithOrOnSyntax<T> WhenTargetHas(Type attributeType)
		{
			if (!typeof(Attribute).IsAssignableFrom(attributeType))
			{
				throw new InvalidOperationException(ExceptionFormatter.InvalidAttributeTypeUsedInBindingCondition(this.Binding, "WhenTargetHas", attributeType));
			}
			this.Binding.Condition = ((IRequest r) => r.Target != null && r.Target.HasAttribute(attributeType));
			return this;
		}
		public IBindingInNamedWithOrOnSyntax<T> WhenParentNamed(string name)
		{
			string.Intern(name);
			this.Binding.Condition = ((IRequest r) => r.ParentContext != null && string.Equals(r.ParentContext.Binding.Metadata.Name, name, StringComparison.Ordinal));
			return this;
		}
		public IBindingWithSyntax<T> Named(string name)
		{
			string.Intern(name);
			this.Binding.Metadata.Name = name;
			return this;
		}
		public IBindingNamedWithOrOnSyntax<T> InSingletonScope()
		{
			this.Binding.ScopeCallback = StandardScopeCallbacks.Singleton;
			return this;
		}
		public IBindingNamedWithOrOnSyntax<T> InTransientScope()
		{
			this.Binding.ScopeCallback = StandardScopeCallbacks.Transient;
			return this;
		}
		public IBindingNamedWithOrOnSyntax<T> InThreadScope()
		{
			this.Binding.ScopeCallback = StandardScopeCallbacks.Thread;
			return this;
		}
		public IBindingNamedWithOrOnSyntax<T> InRequestScope()
		{
			this.Binding.ScopeCallback = StandardScopeCallbacks.Request;
			return this;
		}
		public IBindingNamedWithOrOnSyntax<T> InScope(Func<IContext, object> scope)
		{
			this.Binding.ScopeCallback = scope;
			return this;
		}
		public IBindingWithOrOnSyntax<T> WithConstructorArgument(string name, object value)
		{
			this.Binding.Parameters.Add(new ConstructorArgument(name, value));
			return this;
		}
		public IBindingWithOrOnSyntax<T> WithConstructorArgument(string name, Func<IContext, object> callback)
		{
			this.Binding.Parameters.Add(new ConstructorArgument(name, callback));
			return this;
		}
		public IBindingWithOrOnSyntax<T> WithPropertyValue(string name, object value)
		{
			this.Binding.Parameters.Add(new PropertyValue(name, value));
			return this;
		}
		public IBindingWithOrOnSyntax<T> WithPropertyValue(string name, Func<IContext, object> callback)
		{
			this.Binding.Parameters.Add(new PropertyValue(name, callback));
			return this;
		}
		public IBindingWithOrOnSyntax<T> WithParameter(IParameter parameter)
		{
			this.Binding.Parameters.Add(parameter);
			return this;
		}
		public IBindingWithOrOnSyntax<T> WithMetadata(string key, object value)
		{
			this.Binding.Metadata.Set(key, value);
			return this;
		}
		public IBindingOnSyntax<T> OnActivation(Action<T> action)
		{
			this.Binding.ActivationActions.Add(delegate(object instance)
			{
				action((T)((object)instance));
			});
			return this;
		}
		public IBindingOnSyntax<T> OnDeactivation(Action<T> action)
		{
			this.Binding.DeactivationActions.Add(delegate(object instance)
			{
				action((T)((object)instance));
			});
			return this;
		}
		Type IFluentSyntax.GetType()
		{
			return base.GetType();
		}
	}
}
