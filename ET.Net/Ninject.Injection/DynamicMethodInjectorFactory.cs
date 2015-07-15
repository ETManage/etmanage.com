using Ninject.Components;
using System;
using System.Reflection;
using System.Reflection.Emit;
namespace Ninject.Injection
{
	public class DynamicMethodInjectorFactory : NinjectComponent, IInjectorFactory, INinjectComponent, IDisposable
	{
		public ConstructorInjector Create(ConstructorInfo constructor)
		{
			DynamicMethod dynamicMethod = new DynamicMethod(DynamicMethodInjectorFactory.GetAnonymousMethodName(), typeof(object), new Type[]
			{
				typeof(object[])
			}, true);
			ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
			DynamicMethodInjectorFactory.EmitLoadMethodArguments(iLGenerator, constructor);
			iLGenerator.Emit(OpCodes.Newobj, constructor);
			if (constructor.ReflectedType.IsValueType)
			{
				iLGenerator.Emit(OpCodes.Box, constructor.ReflectedType);
			}
			iLGenerator.Emit(OpCodes.Ret);
			return (ConstructorInjector)dynamicMethod.CreateDelegate(typeof(ConstructorInjector));
		}
		public PropertyInjector Create(PropertyInfo property)
		{
			DynamicMethod dynamicMethod = new DynamicMethod(DynamicMethodInjectorFactory.GetAnonymousMethodName(), typeof(void), new Type[]
			{
				typeof(object),
				typeof(object)
			}, true);
			ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
			iLGenerator.Emit(OpCodes.Ldarg_0);
			DynamicMethodInjectorFactory.EmitUnboxOrCast(iLGenerator, property.DeclaringType);
			iLGenerator.Emit(OpCodes.Ldarg_1);
			DynamicMethodInjectorFactory.EmitUnboxOrCast(iLGenerator, property.PropertyType);
			DynamicMethodInjectorFactory.EmitMethodCall(iLGenerator, property.GetSetMethod(base.Settings.InjectNonPublic));
			iLGenerator.Emit(OpCodes.Ret);
			return (PropertyInjector)dynamicMethod.CreateDelegate(typeof(PropertyInjector));
		}
		public MethodInjector Create(MethodInfo method)
		{
			DynamicMethod dynamicMethod = new DynamicMethod(DynamicMethodInjectorFactory.GetAnonymousMethodName(), typeof(void), new Type[]
			{
				typeof(object),
				typeof(object[])
			}, true);
			ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
			iLGenerator.Emit(OpCodes.Ldarg_0);
			DynamicMethodInjectorFactory.EmitUnboxOrCast(iLGenerator, method.DeclaringType);
			DynamicMethodInjectorFactory.EmitLoadMethodArguments(iLGenerator, method);
			DynamicMethodInjectorFactory.EmitMethodCall(iLGenerator, method);
			if (method.ReturnType != typeof(void))
			{
				iLGenerator.Emit(OpCodes.Pop);
			}
			iLGenerator.Emit(OpCodes.Ret);
			return (MethodInjector)dynamicMethod.CreateDelegate(typeof(MethodInjector));
		}
		private static void EmitLoadMethodArguments(ILGenerator il, MethodBase targetMethod)
		{
			ParameterInfo[] parameters = targetMethod.GetParameters();
			OpCode opcode = (targetMethod is ConstructorInfo) ? OpCodes.Ldarg_0 : OpCodes.Ldarg_1;
			for (int i = 0; i < parameters.Length; i++)
			{
				il.Emit(opcode);
				il.Emit(OpCodes.Ldc_I4, i);
				il.Emit(OpCodes.Ldelem_Ref);
				DynamicMethodInjectorFactory.EmitUnboxOrCast(il, parameters[i].ParameterType);
			}
		}
		private static void EmitMethodCall(ILGenerator il, MethodInfo method)
		{
			OpCode opcode = method.IsFinal ? OpCodes.Call : OpCodes.Callvirt;
			il.Emit(opcode, method);
		}
		private static void EmitUnboxOrCast(ILGenerator il, Type type)
		{
			OpCode opcode = type.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass;
			il.Emit(opcode, type);
		}
		private static string GetAnonymousMethodName()
		{
			return "DynamicInjector" + Guid.NewGuid().ToString("N");
		}
	}
}
