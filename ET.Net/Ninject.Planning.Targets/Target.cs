using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Infrastructure.Language;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Ninject.Planning.Targets
{
	public abstract class Target<T> : ITarget, ICustomAttributeProvider where T : ICustomAttributeProvider
	{
		private readonly Future<Func<IBindingMetadata, bool>> _constraint;
		private readonly Future<bool> _isOptional;
		public MemberInfo Member
		{
			get;
			private set;
		}
		public T Site
		{
			get;
			private set;
		}
		public abstract string Name
		{
			get;
		}
		public abstract Type Type
		{
			get;
		}
		public Func<IBindingMetadata, bool> Constraint
		{
			get
			{
				return this._constraint;
			}
		}
		public bool IsOptional
		{
			get
			{
				return this._isOptional;
			}
		}
		protected Target(MemberInfo member, T site)
		{
			Ensure.ArgumentNotNull(member, "member");
			Ensure.ArgumentNotNull(site, "site");
			this.Member = member;
			this.Site = site;
            //this._constraint = new Future<Func<IBindingMetadata, bool>>(new Func<Func<IBindingMetadata, bool>>(this.ReadConstraintFromTarget));
			this._isOptional = new Future<bool>(new Func<bool>(this.ReadOptionalFromTarget));
		}
		public object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			Ensure.ArgumentNotNull(attributeType, "attributeType");
			T site = this.Site;
			return site.GetCustomAttributes(attributeType, inherit);
		}
		public object[] GetCustomAttributes(bool inherit)
		{
			T site = this.Site;
			return site.GetCustomAttributes(inherit);
		}
		public bool IsDefined(Type attributeType, bool inherit)
		{
			Ensure.ArgumentNotNull(attributeType, "attributeType");
			T site = this.Site;
			return site.IsDefined(attributeType, inherit);
		}
		public object ResolveWithin(IContext parent)
		{
			Ensure.ArgumentNotNull(parent, "parent");
			if (this.Type.IsArray)
			{
				Type elementType = this.Type.GetElementType();
				return this.GetValues(elementType, parent).CastSlow(elementType).ToArraySlow(elementType);
			}
			if (this.Type.IsGenericType)
			{
				Type genericTypeDefinition = this.Type.GetGenericTypeDefinition();
				Type type = this.Type.GetGenericArguments()[0];
				if (genericTypeDefinition == typeof(List<>) || genericTypeDefinition == typeof(IList<>) || genericTypeDefinition == typeof(ICollection<>))
				{
					return this.GetValues(type, parent).CastSlow(type).ToListSlow(type);
				}
				if (genericTypeDefinition == typeof(IEnumerable<>))
				{
					return this.GetValues(type, parent).CastSlow(type);
				}
			}
			return this.GetValues(this.Type, parent).FirstOrDefault<object>();
		}
		protected virtual IEnumerable<object> GetValues(Type service, IContext parent)
		{
			Ensure.ArgumentNotNull(service, "service");
			Ensure.ArgumentNotNull(parent, "parent");
			IRequest request = parent.Request.CreateChild(service, parent, this);
			return parent.Kernel.Resolve(request);
		}
		protected virtual bool ReadOptionalFromTarget()
		{
			return this.Site.HasAttribute(typeof(OptionalAttribute));
		}
        //protected virtual Func<IBindingMetadata, bool> ReadConstraintFromTarget()
        //{
        //    Target<T> c__DisplayClass = new Target<T>();
        //    Target<T> arg_2B_0 = c__DisplayClass;
        //    T site = this.Site;
        //    arg_2B_0.attributes = (site.GetCustomAttributes(typeof(ConstraintAttribute), true) as ConstraintAttribute[]);
        //    if (c__DisplayClass.attributes == null || c__DisplayClass.attributes.Length == 0)
        //    {
        //        return null;
        //    }
        //    if (c__DisplayClass.attributes.Length == 1)
        //    {
        //        return new Func<IBindingMetadata, bool>(c__DisplayClass.attributes[0].Matches);
        //    }
        //    return (IBindingMetadata metadata) => c__DisplayClass.attributes.All((ConstraintAttribute attribute) => attribute.Matches(metadata));
        //}
	}
}
