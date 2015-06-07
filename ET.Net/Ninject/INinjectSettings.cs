using System;
namespace Ninject
{
	public interface INinjectSettings
	{
		Type InjectAttribute
		{
			get;
		}
		TimeSpan CachePruningInterval
		{
			get;
		}
		bool LoadExtensions
		{
			get;
		}
		string ExtensionSearchPattern
		{
			get;
		}
		bool UseReflectionBasedInjection
		{
			get;
		}
		bool InjectNonPublic
		{
			get;
			set;
		}
		T Get<T>(string key, T defaultValue);
		void Set(string key, object value);
	}
}
