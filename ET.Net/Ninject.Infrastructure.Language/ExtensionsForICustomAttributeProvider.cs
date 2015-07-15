using System;
using System.Reflection;
namespace Ninject.Infrastructure.Language
{
	internal static class ExtensionsForICustomAttributeProvider
	{
		public static bool HasAttribute(this ICustomAttributeProvider member, Type type)
		{
			return member.IsDefined(type, true);
		}
	}
}
