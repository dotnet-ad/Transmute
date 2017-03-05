namespace Transmute
{
	public static class StringConverters
	{
		public static void Register(Transmuter transmuter)
		{
			//to string
			transmuter.Register(new RelayConverter<string, short>(x => short.Parse(x, System.Globalization.NumberStyles.Any)));
			transmuter.Register(new RelayConverter<string, int>(x => int.Parse(x, System.Globalization.NumberStyles.Any)));
			transmuter.Register(new RelayConverter<string, long>(x => long.Parse(x, System.Globalization.NumberStyles.Any)));
			transmuter.Register(new RelayConverter<string, bool>(x => bool.Parse(x)));
			transmuter.Register(new RelayConverter<string, byte>(x => byte.Parse(x)));
			transmuter.Register(new RelayConverter<string, float>(x => float.Parse(x, System.Globalization.NumberStyles.Any)));
			transmuter.Register(new RelayConverter<string, double>(x => double.Parse(x, System.Globalization.NumberStyles.Any)));
		}
	}
}
