using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Ninject
{
	public static class ResolutionExtensions
	{
		public static T Get<T>(this IResolutionRoot root, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, typeof(T), null, parameters, false, true).Cast<T>().Single<T>();
		}
		public static T Get<T>(this IResolutionRoot root, string name, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, typeof(T), (IBindingMetadata b) => b.Name == name, parameters, false, true).Cast<T>().Single<T>();
		}
		public static T Get<T>(this IResolutionRoot root, Func<IBindingMetadata, bool> constraint, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, typeof(T), constraint, parameters, false, true).Cast<T>().Single<T>();
		}
		public static T TryGet<T>(this IResolutionRoot root, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, typeof(T), null, parameters, true, true).Cast<T>().SingleOrDefault<T>();
		}
		public static T TryGet<T>(this IResolutionRoot root, string name, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, typeof(T), (IBindingMetadata b) => b.Name == name, parameters, true, true).Cast<T>().SingleOrDefault<T>();
		}
		public static T TryGet<T>(this IResolutionRoot root, Func<IBindingMetadata, bool> constraint, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, typeof(T), constraint, parameters, true, true).Cast<T>().SingleOrDefault<T>();
		}
		public static IEnumerable<T> GetAll<T>(this IResolutionRoot root, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, typeof(T), null, parameters, true, false).Cast<T>();
		}
		public static IEnumerable<T> GetAll<T>(this IResolutionRoot root, string name, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, typeof(T), (IBindingMetadata b) => b.Name == name, parameters, true, false).Cast<T>();
		}
		public static IEnumerable<T> GetAll<T>(this IResolutionRoot root, Func<IBindingMetadata, bool> constraint, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, typeof(T), constraint, parameters, true, false).Cast<T>();
		}
		public static object Get(this IResolutionRoot root, Type service, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, service, null, parameters, false, true).Single<object>();
		}
		public static object Get(this IResolutionRoot root, Type service, string name, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, service, (IBindingMetadata b) => b.Name == name, parameters, false, true).Single<object>();
		}
		public static object Get(this IResolutionRoot root, Type service, Func<IBindingMetadata, bool> constraint, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, service, constraint, parameters, false, true).Single<object>();
		}
		public static object TryGet(this IResolutionRoot root, Type service, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, service, null, parameters, true, true).SingleOrDefault<object>();
		}
		public static object TryGet(this IResolutionRoot root, Type service, string name, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, service, (IBindingMetadata b) => b.Name == name, parameters, true, false).FirstOrDefault<object>();
		}
		public static object TryGet(this IResolutionRoot root, Type service, Func<IBindingMetadata, bool> constraint, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, service, constraint, parameters, true, false).FirstOrDefault<object>();
		}
		public static IEnumerable<object> GetAll(this IResolutionRoot root, Type service, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, service, null, parameters, true, false);
		}
		public static IEnumerable<object> GetAll(this IResolutionRoot root, Type service, string name, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, service, (IBindingMetadata b) => b.Name == name, parameters, true, false);
		}
		public static IEnumerable<object> GetAll(this IResolutionRoot root, Type service, Func<IBindingMetadata, bool> constraint, params IParameter[] parameters)
		{
			return ResolutionExtensions.GetResolutionIterator(root, service, constraint, parameters, true, false);
		}
		private static IEnumerable<object> GetResolutionIterator(IResolutionRoot root, Type service, Func<IBindingMetadata, bool> constraint, IEnumerable<IParameter> parameters, bool isOptional, bool isUnique)
		{
			Ensure.ArgumentNotNull(root, "root");
			Ensure.ArgumentNotNull(service, "service");
			Ensure.ArgumentNotNull(parameters, "parameters");
			IRequest request = root.CreateRequest(service, constraint, parameters, isOptional, isUnique);
			return root.Resolve(request);
		}
	}
}
