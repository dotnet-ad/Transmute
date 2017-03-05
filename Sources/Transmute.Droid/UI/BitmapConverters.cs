namespace Transmute
{
	using System;
	using Android.Graphics;

	public static class BitmapConverters
	{
		public static void Register(Transmuter transmuter)
		{
			transmuter.Register(FromString());
		}

		private static int CalculateInSampleSize(
			BitmapFactory.Options options, int reqWidth, int reqHeight)
		{
			// Raw height and width of image
			int height = options.OutHeight;
			int width = options.OutWidth;
			int inSampleSize = 1;

			if (height > reqHeight || width > reqWidth)
			{

				int halfHeight = height / 2;
				int halfWidth = width / 2;

				// Calculate the largest inSampleSize value that is a power of 2 and keeps both
				// height and width larger than the requested height and width.
				while ((halfHeight / inSampleSize) >= reqHeight
						&& (halfWidth / inSampleSize) >= reqWidth)
				{
					inSampleSize *= 2;
				}
			}

			return inSampleSize;
		}

		#region string

		public static IConverter<string, Bitmap> FromString() => new RelayConverter<string, Bitmap>((value) =>
		{
			if (string.IsNullOrEmpty(value))
				return null;

			try
			{
				var options = new BitmapFactory.Options();
				return BitmapFactory.DecodeFile(value, options);
			}
			catch (Exception)
			{
				return null;
			}

		});

		#endregion
	}

}
