namespace Transmute
{
	using System;

	public static class NumericConverters
	{
		public static void Register(Transmuter transmuter)
		{
			//bytes
			transmuter.Register(new RelayConverter<short, byte[]>(x => BitConverter.GetBytes(x)));
			transmuter.Register(new RelayConverter<int, byte[]>(x => BitConverter.GetBytes(x)));
			transmuter.Register(new RelayConverter<long, byte[]>(x => BitConverter.GetBytes(x)));
			transmuter.Register(new RelayConverter<float, byte[]>(x => BitConverter.GetBytes(x)));
			transmuter.Register(new RelayConverter<double, byte[]>(x => BitConverter.GetBytes(x)));
			transmuter.Register(new RelayConverter<bool, byte[]>(x => BitConverter.GetBytes(x)));
			transmuter.Register(new RelayConverter<byte[], short>(x => BitConverter.ToInt16(x, 0)));
			transmuter.Register(new RelayConverter<byte[], int>(x => BitConverter.ToInt32(x, 0)));
			transmuter.Register(new RelayConverter<byte[], long>(x => BitConverter.ToInt64(x, 0)));
			transmuter.Register(new RelayConverter<byte[], float>(x => BitConverter.ToSingle(x, 0)));
			transmuter.Register(new RelayConverter<byte[], double>(x => BitConverter.ToDouble(x, 0)));
			transmuter.Register(new RelayConverter<byte[], bool>(x => BitConverter.ToBoolean(x,0)));

			//Casts
			transmuter.Register(new CastConverter<int, short>());
			transmuter.Register(new CastConverter<short, int>());
			transmuter.Register(new CastConverter<int, long>());
			transmuter.Register(new CastConverter<long, int>());
			transmuter.Register(new CastConverter<int, float>());
			transmuter.Register(new CastConverter<float, int>());
			transmuter.Register(new CastConverter<int, double>());
			transmuter.Register(new CastConverter<double, int>());
			transmuter.Register(new CastConverter<float, double>());
			transmuter.Register(new CastConverter<double, float>());

			//bool
			transmuter.Register(new RelayConverter<int, bool>(x => x > 0));
			transmuter.Register(new RelayConverter<bool, int>(x => x ? 1 : 0));

			//ToString
			transmuter.Register(new ToStringConverter<int>());
			transmuter.Register(new ToStringConverter<float>());
			transmuter.Register(new ToStringConverter<double>());
			transmuter.Register(new ToStringConverter<bool>());
		}
	}
}
