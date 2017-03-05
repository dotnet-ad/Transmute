namespace Transmute
{
	public partial class Transmuter
	{
		public void RegisterBase()
		{
			NumericConverters.Register(this);
			DateTimeConverters.Register(this);
		}
	}
}
