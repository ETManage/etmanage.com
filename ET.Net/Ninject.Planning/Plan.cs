using Ninject.Infrastructure;
using Ninject.Planning.Directives;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Ninject.Planning
{
	public class Plan : IPlan
	{
		public Type Type
		{
			get;
			private set;
		}
		public ICollection<IDirective> Directives
		{
			get;
			private set;
		}
		public Plan(Type type)
		{
			Ensure.ArgumentNotNull(type, "type");
			this.Type = type;
			this.Directives = new List<IDirective>();
		}
		public void Add(IDirective directive)
		{
			Ensure.ArgumentNotNull(directive, "directive");
			this.Directives.Add(directive);
		}
		public bool Has<TDirective>() where TDirective : IDirective
		{
			return this.GetAll<TDirective>().Count<TDirective>() > 0;
		}
		public TDirective GetOne<TDirective>() where TDirective : IDirective
		{
			return this.GetAll<TDirective>().SingleOrDefault<TDirective>();
		}
		public IEnumerable<TDirective> GetAll<TDirective>() where TDirective : IDirective
		{
			return this.Directives.OfType<TDirective>();
		}
	}
}
