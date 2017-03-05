namespace Transmute
{
	public partial class Transmuter
	{
		public void RegisterPlatform()
		{
			NativeConverters.Register(this);
			NSDateConverters.Register(this);
			UIColorConverters.Register(this);
			CGRectConverters.Register(this);
			UIImageConverters.Register(this);
		}
	}
}
