using Ninject.Activation;
using System;
namespace Ninject.Parameters
{
	public class ConstructorArgument : Parameter
	{
		public ConstructorArgument(string name, object value) : base(name, value, false)
		{
		}
		public ConstructorArgument(string name, Func<IContext, object> valueCallback) : base(name, valueCallback, false)
		{
		}
	}
}
