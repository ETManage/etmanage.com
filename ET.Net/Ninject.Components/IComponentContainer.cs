using System;
using System.Collections.Generic;
namespace Ninject.Components
{
	public interface IComponentContainer : IDisposable
	{
		IKernel Kernel
		{
			get;
			set;
		}
		void Add<TComponent, TImplementation>() where TComponent : INinjectComponent where TImplementation : TComponent, INinjectComponent;
		void RemoveAll<T>() where T : INinjectComponent;
		void RemoveAll(Type component);
		T Get<T>() where T : INinjectComponent;
		IEnumerable<T> GetAll<T>() where T : INinjectComponent;
		object Get(Type component);
		IEnumerable<object> GetAll(Type component);
	}
}
