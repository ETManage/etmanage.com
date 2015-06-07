using Ninject.Infrastructure.Disposal;
using System;
namespace Ninject.Components
{
	public abstract class NinjectComponent : DisposableObject, INinjectComponent, IDisposable
	{
		public INinjectSettings Settings
		{
			get;
			set;
		}
	}
}
