using Ninject.Activation;
using System;
namespace Ninject.Parameters
{
	public interface IParameter : IEquatable<IParameter>
	{
		string Name
		{
			get;
		}
		bool ShouldInherit
		{
			get;
		}
		object GetValue(IContext context);
	}
}
