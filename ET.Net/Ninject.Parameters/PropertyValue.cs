using Ninject.Activation;
using System;
namespace Ninject.Parameters
{
	public class PropertyValue : Parameter
	{
		public PropertyValue(string name, object value) : base(name, value, false)
		{
		}
		public PropertyValue(string name, Func<IContext, object> valueCallback) : base(name, valueCallback, false)
		{
		}
	}
}
