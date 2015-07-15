using Ninject.Activation;
using Ninject.Planning.Bindings;
using Ninject.Planning.Targets;
using System;
using System.IO;
using System.Reflection;
using System.Text;
namespace Ninject.Infrastructure.Introspection
{
	internal static class FormatExtensions
	{
		public static string FormatActivationPath(this IRequest request)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				for (IRequest request2 = request; request2 != null; request2 = request2.ParentRequest)
				{
					stringWriter.WriteLine("{0,3}) {1}", request2.Depth + 1, request2.Format());
				}
				result = stringWriter.ToString();
			}
			return result;
		}
		public static string Format(this IBinding binding, IContext context)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				if (binding.Condition != null)
				{
					stringWriter.Write("conditional ");
				}
				if (binding.IsImplicit)
				{
					stringWriter.Write("implicit ");
				}
				IProvider provider = binding.GetProvider(context);
				switch (binding.Target)
				{
				case BindingTarget.Self:
					stringWriter.Write("self-binding of {0}", binding.Service.Format());
					break;
				case BindingTarget.Type:
					stringWriter.Write("binding from {0} to {1}", binding.Service.Format(), provider.Type.Format());
					break;
				case BindingTarget.Provider:
					stringWriter.Write("provider binding from {0} to {1} (via {2})", binding.Service.Format(), provider.Type.Format(), provider.GetType().Format());
					break;
				case BindingTarget.Method:
					stringWriter.Write("binding from {0} to method", binding.Service.Format());
					break;
				case BindingTarget.Constant:
					stringWriter.Write("binding from {0} to constant value", binding.Service.Format());
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				result = stringWriter.ToString();
			}
			return result;
		}
		public static string Format(this IRequest request)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				if (request.Target == null)
				{
					stringWriter.Write("Request for {0}", request.Service.Format());
				}
				else
				{
					stringWriter.Write("Injection of dependency {0} into {1}", request.Service.Format(), request.Target.Format());
				}
				result = stringWriter.ToString();
			}
			return result;
		}
		public static string Format(this ITarget target)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				MemberTypes memberType = target.Member.MemberType;
				if (memberType != MemberTypes.Constructor)
				{
					if (memberType != MemberTypes.Method)
					{
						if (memberType != MemberTypes.Property)
						{
							throw new ArgumentOutOfRangeException();
						}
						stringWriter.Write("property {0}", target.Name);
					}
					else
					{
						stringWriter.Write("parameter {0} of method {1}", target.Name, target.Member.Name);
					}
				}
				else
				{
					stringWriter.Write("parameter {0} of constructor", target.Name);
				}
				stringWriter.Write(" of type {0}", target.Member.ReflectedType.Format());
				result = stringWriter.ToString();
			}
			return result;
		}
		public static string Format(this Type type)
		{
			if (type.IsGenericType)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(type.Name.Substring(0, type.Name.LastIndexOf('`')));
				stringBuilder.Append("{");
				Type[] genericArguments = type.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					Type type2 = genericArguments[i];
					stringBuilder.Append(type2.Format());
					stringBuilder.Append(", ");
				}
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
				stringBuilder.Append("}");
				return stringBuilder.ToString();
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return "bool";
			case TypeCode.Char:
				return "char";
			case TypeCode.SByte:
				return "sbyte";
			case TypeCode.Byte:
				return "byte";
			case TypeCode.Int16:
				return "short";
			case TypeCode.UInt16:
				return "ushort";
			case TypeCode.Int32:
				return "int";
			case TypeCode.UInt32:
				return "uint";
			case TypeCode.Int64:
				return "long";
			case TypeCode.UInt64:
				return "ulong";
			case TypeCode.Single:
				return "float";
			case TypeCode.Double:
				return "double";
			case TypeCode.Decimal:
				return "decimal";
			case TypeCode.DateTime:
				return "DateTime";
			case TypeCode.String:
				return "string";
			}
			return type.Name;
		}
	}
}
