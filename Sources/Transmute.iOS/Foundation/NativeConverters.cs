namespace Transmute
{
	using System;
	using Foundation;

	public static class NativeConverters
	{
		public static void Register(Transmuter transmuter)
		{
			//Casts
			transmuter.Register(new CastConverter<nint, int>());
			transmuter.Register(new CastConverter<int, nint>());
			transmuter.Register(new CastConverter<nint, float>());
			transmuter.Register(new CastConverter<float, nint>());
			transmuter.Register(new CastConverter<nint, double>());
			transmuter.Register(new CastConverter<double, nint>());
			transmuter.Register(new CastConverter<nfloat, int>());
			transmuter.Register(new CastConverter<int, nfloat>());
			transmuter.Register(new CastConverter<nfloat, float>());
			transmuter.Register(new CastConverter<float, nfloat>());
			transmuter.Register(new CastConverter<nfloat, double>());
			transmuter.Register(new CastConverter<double, nfloat>());
		}
	}
}
