using Ninject.Activation;
using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Planning.Directives;
using Ninject.Planning.Targets;
using System;
using System.Collections;
using System.Linq;
namespace Ninject.Selection.Heuristics
{
	public class StandardConstructorScorer : NinjectComponent, IConstructorScorer, INinjectComponent, IDisposable
	{
		public int Score(IContext context, ConstructorInjectionDirective directive)
		{
			Ensure.ArgumentNotNull(context, "context");
			Ensure.ArgumentNotNull(directive, "constructor");
			if (directive.Constructor.HasAttribute(base.Settings.InjectAttribute))
			{
				return 2147483647;
			}
			int num = 1;
			ITarget[] targets = directive.Targets;
			for (int i = 0; i < targets.Length; i++)
			{
				ITarget target = targets[i];
				foreach (IParameter current in context.Parameters)
				{
					if (string.Equals(target.Name, current.Name))
					{
						num++;
					}
				}
				Type type2 = target.Type;
				if (type2.IsArray)
				{
					type2 = type2.GetElementType();
				}
				if (type2.IsGenericType)
				{
					if (type2.GetInterfaces().Any((Type type) => type == typeof(IEnumerable)))
					{
						type2 = type2.GetGenericArguments()[0];
					}
				}
				if (context.Kernel.GetBindings(type2).Count<IBinding>() > 0)
				{
					num++;
				}
			}
			return num;
		}
	}
}
