using System;
namespace Ninject.Planning.Bindings
{
	public interface IBindingMetadata
	{
		string Name
		{
			get;
			set;
		}
		bool Has(string key);
		T Get<T>(string key);
		T Get<T>(string key, T defaultValue);
		void Set(string key, object value);
	}
}
