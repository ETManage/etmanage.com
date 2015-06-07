using System;
using System.Reflection;
namespace Ninject.Infrastructure.Language
{
	internal static class ExtensionsForTargetInvocationException
	{
		public static void RethrowInnerException(this TargetInvocationException exception)
		{
			Exception innerException = exception.InnerException;
			FieldInfo field = typeof(Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic);
			field.SetValue(innerException, innerException.StackTrace);
			throw innerException;
		}
	}
}
