namespace Transmute
{
	public partial class Transmuter
	{
		public void RegisterBase()
		{
			BaseConverters.Register(this);
			StringConverters.Register(this);
			DateTimeConverters.Register(this);
		}
	}
}
