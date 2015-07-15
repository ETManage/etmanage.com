using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using System;
using System.Reflection;
namespace Ninject.Selection.Heuristics
{
	public class StandardInjectionHeuristic : NinjectComponent, IInjectionHeuristic, INinjectComponent, IDisposable
	{
		public bool ShouldInject(MemberInfo member)
		{
			Ensure.ArgumentNotNull(member, "member");
			return member.HasAttribute(base.Settings.InjectAttribute);
		}
	}
}
