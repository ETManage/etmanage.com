using Ninject.Activation;
using Ninject.Infrastructure;
using System;
namespace Ninject.Syntax
{
	public interface IBindingWhenSyntax<T> : IBindingSyntax, IFluentSyntax, IHaveBinding, IHaveKernel
	{
		IBindingInNamedWithOrOnSyntax<T> When(Func<IRequest, bool> condition);
		IBindingInNamedWithOrOnSyntax<T> WhenInjectedInto<TParent>();
		IBindingInNamedWithOrOnSyntax<T> WhenInjectedInto(Type parent);
		IBindingInNamedWithOrOnSyntax<T> WhenClassHas<TAttribute>() where TAttribute : Attribute;
		IBindingInNamedWithOrOnSyntax<T> WhenMemberHas<TAttribute>() where TAttribute : Attribute;
		IBindingInNamedWithOrOnSyntax<T> WhenTargetHas<TAttribute>() where TAttribute : Attribute;
		IBindingInNamedWithOrOnSyntax<T> WhenClassHas(Type attributeType);
		IBindingInNamedWithOrOnSyntax<T> WhenMemberHas(Type attributeType);
		IBindingInNamedWithOrOnSyntax<T> WhenTargetHas(Type attributeType);
		IBindingInNamedWithOrOnSyntax<T> WhenParentNamed(string name);
	}
}
