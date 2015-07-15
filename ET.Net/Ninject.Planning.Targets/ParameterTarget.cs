using System;
using System.Reflection;
namespace Ninject.Planning.Targets
{
	public class ParameterTarget : Target<ParameterInfo>
	{
		public override string Name
		{
			get
			{
				return base.Site.Name;
			}
		}
		public override Type Type
		{
			get
			{
				return base.Site.ParameterType;
			}
		}
		public ParameterTarget(MethodBase method, ParameterInfo site) : base(method, site)
		{
		}
	}
}
