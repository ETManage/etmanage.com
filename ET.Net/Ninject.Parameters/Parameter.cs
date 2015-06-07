using Ninject.Activation;
using Ninject.Infrastructure;
using System;
namespace Ninject.Parameters
{
	public class Parameter : IParameter, IEquatable<IParameter>
	{
		public string Name
		{
			get;
			private set;
		}
		public bool ShouldInherit
		{
			get;
			private set;
		}
		public Func<IContext, object> ValueCallback
		{
			get;
			private set;
		}
		public Parameter(string name, object value, bool shouldInherit) : this(name, (IContext ctx) => value, shouldInherit)
		{
		}
		public Parameter(string name, Func<IContext, object> valueCallback, bool shouldInherit)
		{
			Ensure.ArgumentNotNullOrEmpty(name, "name");
			Ensure.ArgumentNotNull(valueCallback, "valueCallback");
			this.Name = name;
			this.ValueCallback = valueCallback;
			this.ShouldInherit = shouldInherit;
		}
		public object GetValue(IContext context)
		{
			Ensure.ArgumentNotNull(context, "context");
			return this.ValueCallback(context);
		}
		public override bool Equals(object obj)
		{
			IParameter parameter = obj as IParameter;
			if (parameter == null)
			{
				return base.Equals(obj);
			}
			return this.Equals(parameter);
		}
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Name.GetHashCode();
		}
		public bool Equals(IParameter other)
		{
			return other.GetType() == base.GetType() && other.Name.Equals(this.Name);
		}
	}
}
