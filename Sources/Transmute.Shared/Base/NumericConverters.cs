using System;

namespace Transmute
{
	public static class NumericConverters
	{
		public static void Register(Transmuter transmuter)
		{
			transmuter.Register(new CastConverter<int, float>());
			transmuter.Register(new CastConverter<float, int>());
			transmuter.Register(new CastConverter<int, double>());
			transmuter.Register(new CastConverter<double, int>());
			transmuter.Register(new CastConverter<float, double>());
			transmuter.Register(new CastConverter<double, float>());

			//bool
			transmuter.Register(new RelayConverter<int, bool>(x => x > 0));
			transmuter.Register(new RelayConverter<float, bool>(x => x > 0));
			transmuter.Register(new RelayConverter<double, bool>(x => x > 0));
			transmuter.Register(new RelayConverter<bool, int>(x => x ? 1 : 0));
			transmuter.Register(new RelayConverter<bool, float>(x => x ? 1 : 0));
			transmuter.Register(new RelayConverter<bool, double>(x => x ? 1 : 0));

			//ToString
			transmuter.Register(new ToStringConverter<int>());
			transmuter.Register(new ToStringConverter<float>());
			transmuter.Register(new ToStringConverter<double>());
			transmuter.Register(new ToStringConverter<bool>());
		}

	}

}
