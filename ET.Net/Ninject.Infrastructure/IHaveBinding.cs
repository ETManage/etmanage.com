using Ninject.Planning.Bindings;
using System;
namespace Ninject.Infrastructure
{
	public interface IHaveBinding
	{
		IBinding Binding
		{
			get;
		}
	}
}
