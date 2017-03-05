namespace Transmute
{
	public partial class Transmuter
	{
		public void RegisterPlatform()
		{
			ViewStatesConverters.Register(this);
			ColorConverters.Register(this);
			BitmapConverters.Register(this);
		}
	}
}
