using System;
namespace Ninject.Infrastructure
{
	[Flags]
	public enum RequestFlags
	{
		Single = 0,
		Multiple = 1,
		Optional = 2
	}
}
