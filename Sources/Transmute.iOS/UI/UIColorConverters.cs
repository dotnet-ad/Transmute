namespace Transmute
{
	using System;
	using UIKit;

	public static class UIColorConverters
	{
		public static void Register(Transmuter transmuter)
		{
			transmuter.Register(FromInt());
			transmuter.Register(ToInt());
			transmuter.Register(FromBool());
			transmuter.Register(ToBool());
		}

		#region int

		public static IConverter<int, UIColor> FromInt() => new RelayConverter<int, UIColor>((value) =>
		 {
			 var a = ((nfloat)((value & 0xFF000000) >> 32)) / 255.0f;
			 var r = ((nfloat)((value & 0x00FF00000) >> 16)) / 255.0f;
			 var g = ((nfloat)((value & 0x0000FF00) >> 8)) / 255.0f;
			 var b = ((nfloat)(value & 0x000000FF)) / 255.0f;
			 return UIColor.FromRGBA(r, g, b, a);
		 });

		public static IConverter<UIColor, int> ToInt() => new RelayConverter<UIColor, int>((value) =>
		 {
			 var a = (int)(value.CGColor.Alpha * 255);
			 var r = (int)(value.CGColor.Components[0] * 255);
			 var g = (int)(value.CGColor.Components[1] * 255);
			 var b = (int)(value.CGColor.Components[2] * 255);
			 return (a << 32) + (r << 16) + (g << 8) + b;
		 });

		#endregion

		#region bool

		public static IConverter<bool, UIColor> FromBool() => FromBool(UIColor.Green, UIColor.Red);

		public static IConverter<bool, UIColor> FromBool(UIColor active, UIColor unactive) => new RelayConverter<bool, UIColor>((value) =>
		 {
			return value ? active : unactive;
		 });

		public static IConverter<UIColor, bool> ToBool() => ToBool(UIColor.Green);

		public static IConverter<UIColor, bool> ToBool(UIColor active) => new RelayConverter<UIColor, bool>((value) =>
		 {
			 return value == active;
		 });

		#endregion
	}

}
