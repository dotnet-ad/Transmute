namespace Transmute
{
	using System;

	public partial class Transmuter
	{
		public void RegisterPlatform()
		{
			UIColorConverters.Register(this);
			CGRectConverters.Register(this);
		}
	}
}
