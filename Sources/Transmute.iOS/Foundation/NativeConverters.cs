namespace Transmute
{
	using System;
	using Foundation;

	public static class NativeConverters
	{
		public static void Register(Transmuter transmuter)
		{
			//Casts
			transmuter.Register(new RelayConverter<nint, int>(x => (int)x));
			transmuter.Register(new RelayConverter<int, nint>(x => new nint(x)));
			transmuter.Register(new RelayConverter<nint, float>(x => (int)x));
			transmuter.Register(new RelayConverter<float, nint>(x => new nint((int)x)));
			transmuter.Register(new RelayConverter<nint, double>(x => (int)x));
			transmuter.Register(new RelayConverter<double, nint>(x => new nint((int)x)));
			transmuter.Register(new RelayConverter<nfloat, int>(x => (int)x));
			transmuter.Register(new RelayConverter<int, nfloat>(x => new nfloat(x)));
			transmuter.Register(new RelayConverter<nfloat, float>(x => (float)x));
			transmuter.Register(new RelayConverter<float, nfloat>(x => new nfloat(x)));
			transmuter.Register(new RelayConverter<nfloat, double>(x => x));
			transmuter.Register(new RelayConverter<double, nfloat>(x => new nfloat(x)));
		}
	}
}
