using Ninject.Activation.Blocks;
using Ninject.Components;
using Ninject.Infrastructure.Disposal;
using Ninject.Modules;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Ninject
{
	public interface IKernel : IBindingRoot, IResolutionRoot, IServiceProvider, IDisposableObject, IDisposable
	{
		INinjectSettings Settings
		{
			get;
		}
		IComponentContainer Components
		{
			get;
		}
		IEnumerable<INinjectModule> GetModules();
		bool HasModule(string name);
		void Load(IEnumerable<INinjectModule> modules);
		void Load(IEnumerable<string> filePatterns);
		void Load(IEnumerable<Assembly> assemblies);
		void Unload(string name);
		void Inject(object instance, params IParameter[] parameters);
		bool Release(object instance);
		IEnumerable<IBinding> GetBindings(Type service);
		IActivationBlock BeginBlock();
	}
}
