using System;
using System.Reflection;
namespace Ninject.Planning.Targets
{
	public class PropertyTarget : Target<PropertyInfo>
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
				return base.Site.PropertyType;
			}
		}
		public PropertyTarget(PropertyInfo site) : base(site, site)
		{
		}
	}
}
