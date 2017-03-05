	namespace Transmute
	{
		using System;
		using Foundation;
		using UIKit;
		using System.Threading.Tasks;

		public static class UIImageConverters
		{
			public static void Register(Transmuter transmuter)
			{
				transmuter.Register(FromString());
			}

			#region string

			public static IConverter<string, UIImage> FromString() => new RelayConverter<string, UIImage>((value) =>
			{
				if (string.IsNullOrEmpty(value))
					return null;

				try
				{
					NSError err;
					using (var data = NSData.FromFile(value, NSDataReadingOptions.Mapped, out err))
					{
						return UIImage.LoadFromData(data);
					}
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
			public static IConverter<string, Task<UIImage>> FromStringToCachedImage(TimeSpan expiration) => new RelayConverter<string, Task<UIImage>>(async (value) =>
			 {
				 if (string.IsNullOrEmpty(value))
					 return null;

				 var localPath = await FileCache.Default.DownloadCachedFile(value, expiration);

				// Reading image from cache
				NSError err;
				 using (var data = NSData.FromFile(localPath, NSDataReadingOptions.Mapped, out err))
					 return UIImage.LoadFromData(data);
			 });

			#endregion
		}

	}
