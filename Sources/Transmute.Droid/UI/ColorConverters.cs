namespace Transmute
{
	using Android.Graphics;

	public static class ColorConverters
	{
		public static void Register(Transmuter transmuter)
		{
			transmuter.Register(FromInt());
			transmuter.Register(ToInt());
			transmuter.Register(FromBool());
			transmuter.Register(ToBool());
		}

		#region int

		public static IConverter<int, Color> FromInt() => new RelayConverter<int, Color>((value) =>
		 {
			 var a = (value & 0xFF000000) >> 32;
			 var r = (value & 0x00FF00000) >> 16;
			 var g = (value & 0x0000FF00) >> 8;
			 var b = (value & 0x000000FF);
			return Color.Argb(a,r,g,b);
		 });

		public static IConverter<Color, int> ToInt() => new RelayConverter<Color, int>((value) =>
		 {
			var a = (int)value.A;
			 var r = (int)value.R;
			 var g =(int)value.G;
			 var b = (int)value.B;
			 return (a << 32) + (r << 16) + (g << 8) + b;
		 });

		#endregion

		#region bool

		public static IConverter<bool, Color> FromBool() => FromBool(Color.Green, Color.Red);

		public static IConverter<bool, Color> FromBool(Color active, Color unactive) => new RelayConverter<bool, Color>((value) =>
		 {
			 return value ? active : unactive;
		 });

		public static IConverter<Color, bool> ToBool() => ToBool(Color.Green);

		public static IConverter<Color, bool> ToBool(Color active) => new RelayConverter<Color, bool>((value) =>
		 {
			 return value == active;
		 });

		#endregion
	}

}
