namespace Transmute
{
	public static class StringConverters
	{
		public static void Register(Transmuter transmuter)
		{
			//From string
			transmuter.Register(new RelayConverter<string, short>(x => short.Parse(x, System.Globalization.NumberStyles.Any)));
			transmuter.Register(new RelayConverter<string, int>(x => int.Parse(x, System.Globalization.NumberStyles.Any)));
			transmuter.Register(new RelayConverter<string, long>(x => long.Parse(x, System.Globalization.NumberStyles.Any)));
			transmuter.Register(new RelayConverter<string, bool>(x => bool.Parse(x)));
			transmuter.Register(new RelayConverter<string, byte>(x => byte.Parse(x)));
			transmuter.Register(new RelayConverter<string, float>(x => float.Parse(x, System.Globalization.NumberStyles.Any)));
			transmuter.Register(new RelayConverter<string, double>(x => double.Parse(x, System.Globalization.NumberStyles.Any)));

			//ToString
			transmuter.Register(new ToStringConverter<short>());
			transmuter.Register(new ToStringConverter<int>());
			transmuter.Register(new ToStringConverter<long>());
			transmuter.Register(new ToStringConverter<float>());
			transmuter.Register(new ToStringConverter<double>());
			transmuter.Register(new ToStringConverter<bool>());
		}
	}
}
