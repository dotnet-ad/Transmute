namespace Transmute
{
	public partial class Transmuter
	{
		public void RegisterPlatform()
		{
			NSDateConverters.Register(this);
			UIColorConverters.Register(this);
			CGRectConverters.Register(this);
			UIImageConverters.Register(this);
		}
	}
}
