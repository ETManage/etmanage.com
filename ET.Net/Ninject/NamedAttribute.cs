using Ninject.Infrastructure;
using Ninject.Planning.Bindings;
using System;
namespace Ninject
{
	public class NamedAttribute : ConstraintAttribute
	{
		public string Name
		{
			get;
			private set;
		}
		public NamedAttribute(string name)
		{
			Ensure.ArgumentNotNullOrEmpty(name, "name");
			this.Name = name;
		}
		public override bool Matches(IBindingMetadata metadata)
		{
			Ensure.ArgumentNotNull(metadata, "metadata");
			return metadata.Name == this.Name;
		}
	}
}
