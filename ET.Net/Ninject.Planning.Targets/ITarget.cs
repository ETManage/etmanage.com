using Ninject.Activation;
using Ninject.Planning.Bindings;
using System;
using System.Reflection;
namespace Ninject.Planning.Targets
{
	public interface ITarget : ICustomAttributeProvider
	{
		Type Type
		{
			get;
		}
		string Name
		{
			get;
		}
		MemberInfo Member
		{
			get;
		}
		Func<IBindingMetadata, bool> Constraint
		{
			get;
		}
		bool IsOptional
		{
			get;
		}
		object ResolveWithin(IContext parent);
	}
}
