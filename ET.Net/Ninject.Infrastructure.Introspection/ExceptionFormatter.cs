using Ninject.Activation;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using System;
using System.IO;
namespace Ninject.Infrastructure.Introspection
{
	internal static class ExceptionFormatter
	{
		public static string ModuleWithSameNameIsAlreadyLoaded(INinjectModule newModule, INinjectModule existingModule)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				stringWriter.WriteLine("Error loading module '{0}' of type {1}", newModule.Name, newModule.GetType().Format());
				stringWriter.WriteLine("Another module (of type {0}) with the same name has already been loaded", existingModule.GetType().Format());
				stringWriter.WriteLine("Suggestions:");
				stringWriter.WriteLine("  1) Ensure that you have not accidentally loaded the same module twice.");
				stringWriter.WriteLine("  2) If you are using automatic module loading, ensure you have not manually loaded a module");
				stringWriter.WriteLine("\t that may be found by the module loader.");
				result = stringWriter.ToString();
			}
			return result;
		}
		public static string NoModuleLoadedWithTheSpecifiedName(string name)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				stringWriter.WriteLine("Error unloading module '{0}': no such module has been loaded", name);
				stringWriter.WriteLine("Suggestions:");
				stringWriter.WriteLine("  1) Ensure you have previously loaded the module and the name is spelled correctly.");
				stringWriter.WriteLine("  2) Ensure you have not accidentally created more than one kernel.");
				result = stringWriter.ToString();
			}
			return result;
		}
		public static string CouldNotUniquelyResolveBinding(IRequest request)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				stringWriter.WriteLine("Error activating {0}", request.Service.Format());
				stringWriter.WriteLine("More than one matching bindings are available.");
				stringWriter.WriteLine("Activation path:");
				stringWriter.WriteLine(request.FormatActivationPath());
				stringWriter.WriteLine("Suggestions:");
				stringWriter.WriteLine("  1) Ensure that you have defined a binding for {0} only once.", request.Service.Format());
				result = stringWriter.ToString();
			}
			return result;
		}
		public static string CouldNotResolveBinding(IRequest request)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				stringWriter.WriteLine("Error activating {0}", request.Service.Format());
				stringWriter.WriteLine("No matching bindings are available, and the type is not self-bindable.");
				stringWriter.WriteLine("Activation path:");
				stringWriter.WriteLine(request.FormatActivationPath());
				stringWriter.WriteLine("Suggestions:");
				stringWriter.WriteLine("  1) Ensure that you have defined a binding for {0}.", request.Service.Format());
				stringWriter.WriteLine("  2) If the binding was defined in a module, ensure that the module has been loaded into the kernel.");
				stringWriter.WriteLine("  3) Ensure you have not accidentally created more than one kernel.");
				stringWriter.WriteLine("  4) If you are using automatic module loading, ensure the search path and filters are correct.");
				result = stringWriter.ToString();
			}
			return result;
		}
		public static string CyclicalDependenciesDetected(IContext context)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				stringWriter.WriteLine("Error activating {0} using {1}", context.Request.Service.Format(), context.Binding.Format(context));
				stringWriter.WriteLine("A cyclical dependency was detected between the constructors of two services.");
				stringWriter.WriteLine();
				stringWriter.WriteLine("Activation path:");
				stringWriter.WriteLine(context.Request.FormatActivationPath());
				stringWriter.WriteLine("Suggestions:");
				stringWriter.WriteLine("  1) Ensure that you have not declared a dependency for {0} on any implementations of the service.", context.Request.Service.Format());
				stringWriter.WriteLine("  2) Consider combining the services into a single one to remove the cycle.");
				stringWriter.WriteLine("  3) Use property injection instead of constructor injection, and implement IInitializable");
				stringWriter.WriteLine("\t if you need initialization logic to be run after property values have been injected.");
				result = stringWriter.ToString();
			}
			return result;
		}
		public static string InvalidAttributeTypeUsedInBindingCondition(IBinding binding, string methodName, Type type)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				stringWriter.WriteLine("Error registering binding for {0}", binding.Service.Format());
				stringWriter.WriteLine("The type {0} used in a call to {1}() is not a valid attribute.", type.Format(), methodName);
				stringWriter.WriteLine();
				stringWriter.WriteLine("Suggestions:");
				stringWriter.WriteLine("  1) Ensure that you have passed the correct type.");
				stringWriter.WriteLine("  2) If you have defined your own attribute type, ensure that it extends System.Attribute.");
				stringWriter.WriteLine("  3) To avoid problems with type-safety, use the generic version of the the method instead,");
				stringWriter.WriteLine("\t such as {0}<SomeAttribute>().", methodName);
				result = stringWriter.ToString();
			}
			return result;
		}
		public static string NoConstructorsAvailable(IContext context)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				stringWriter.WriteLine("Error activating {0} using {1}", context.Request.Service.Format(), context.Binding.Format(context));
				stringWriter.WriteLine("No constructor was available to create an instance of the implementation type.");
				stringWriter.WriteLine();
				stringWriter.WriteLine("Activation path:");
				stringWriter.WriteLine(context.Request.FormatActivationPath());
				stringWriter.WriteLine("Suggestions:");
				stringWriter.WriteLine("  1) Ensure that the implementation type has a public constructor.");
				stringWriter.WriteLine("  2) If you have implemented the Singleton pattern, use a binding with InSingletonScope() instead.");
				result = stringWriter.ToString();
			}
			return result;
		}
		public static string NoConstructorsAvailableForComponent(Type component, Type implementation)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				stringWriter.WriteLine("Error loading Ninject component {0}", component.Format());
				stringWriter.WriteLine("No constructor was available to create an instance of the registered implementation type {0}.", implementation.Format());
				stringWriter.WriteLine();
				stringWriter.WriteLine("Suggestions:");
				stringWriter.WriteLine("  1) Ensure that the implementation type has a public constructor.");
				result = stringWriter.ToString();
			}
			return result;
		}
		public static string NoSuchComponentRegistered(Type component)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				stringWriter.WriteLine("Error loading Ninject component {0}", component.Format());
				stringWriter.WriteLine("No such component has been registered in the kernel's component container.");
				stringWriter.WriteLine();
				stringWriter.WriteLine("Suggestions:");
				stringWriter.WriteLine("  1) If you have created a custom subclass for KernelBase, ensure that you have properly");
				stringWriter.WriteLine("\t implemented the AddComponents() method.");
				stringWriter.WriteLine("  2) Ensure that you have not removed the component from the container via a call to RemoveAll().");
				stringWriter.WriteLine("  3) Ensure you have not accidentally created more than one kernel.");
				result = stringWriter.ToString();
			}
			return result;
		}
		public static string CouldNotResolveProperyForValueInjection(IRequest request, string propertyName)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				stringWriter.WriteLine("Error activating {0}", request.Service.Format());
				stringWriter.WriteLine("No matching property {0}.", propertyName);
				stringWriter.WriteLine("Activation path:");
				stringWriter.WriteLine(request.FormatActivationPath());
				stringWriter.WriteLine("Suggestions:");
				stringWriter.WriteLine("  1) Ensure that you have the correct property name.");
				result = stringWriter.ToString();
			}
			return result;
		}
	}
}
