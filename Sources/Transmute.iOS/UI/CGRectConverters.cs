using System;
using CoreGraphics;
using System.Linq;

namespace Transmute
{
	public static class CGRectConverters
	{
		public static void Register(Transmuter transmuter)
		{
			transmuter.Register(FromInts());
			transmuter.Register(ToInts());
			transmuter.Register(FromFloats());
			transmuter.Register(ToFloats());
		}

		#region int

		public static IConverter<int[], CGRect> FromInts() => new RelayConverter<int[], CGRect>((value) =>
		 {
			 if (value.Length == 0) return CGRect.Empty;

			 var x = value.Length > 2 ? value[0] : 0;
			 var y = value.Length > 3 ? value[1] : x;
			 var w = value.Length > 2 ? value[2] : value[0];
			 var h = value.Length > 3 ? value[3] : w;

			return new CGRect(x,y,w,h);
		 });

		public static IConverter<CGRect,int[]> ToInts() => new RelayConverter<CGRect, int[]>((value) =>
		 {
			return new int[] { (int)value.X, (int)value.Y, (int)value.Width, (int)value.Height  };
		 });

		#endregion

		#region float

		public static IConverter<float[], CGRect> FromFloats() => new RelayConverter<float[], CGRect>((value) =>
		 {
			 if (value.Length == 0) return CGRect.Empty;

			 var x = value.Length > 2 ? value[0] : 0;
			 var y = value.Length > 3 ? value[1] : x;
			 var w = value.Length > 2 ? value[2] : value[0];
			 var h = value.Length > 3 ? value[3] : w;

			return new CGRect(x,y, y, w);
		 });

		public static IConverter<CGRect,float[]> ToFloats() => new RelayConverter<CGRect, float[]>((value) =>
		 {
			 return new float[] { (float)value.X, (float)value.Y, (float)value.Width, (float)value.Height };
		 });

		#endregion
	}
}
