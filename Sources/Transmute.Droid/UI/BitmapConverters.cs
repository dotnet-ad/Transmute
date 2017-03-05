namespace Transmute
{
	using System;
	using System.Threading.Tasks;
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

		/// <summary>
		/// Async converter that retrieves an image from an url, stores it in local storage with an expiration date.
		/// </summary>
		/// <returns>The string to cached image.</returns>
		/// <param name="expiration">Expiration.</param>
		public static IConverter<string, Task<Bitmap>> FromStringToCachedImage(TimeSpan expiration, int reqWidth, int reqHeight) => new RelayConverter<string, Task<Bitmap>>(async (value) =>
		 {
			 if (string.IsNullOrEmpty(value))
				 return null;

			var localPath = await FileCache.Default.DownloadCachedFile(value, expiration);

			 // First decode with inJustDecodeBounds=true to check dimensions
			var options = new BitmapFactory.Options();
			options.InJustDecodeBounds = true;
			BitmapFactory.DecodeFile(localPath, options);

			 // Calculate inSampleSize
			options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

			 // Decode bitmap with inSampleSize set
			options.InJustDecodeBounds = false;
			return BitmapFactory.DecodeFile(localPath, options);
		 });

		#endregion
	}

}
