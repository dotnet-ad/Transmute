namespace Transmute
{
	using System;
	using Foundation;
	using UIKit;

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

		#endregion
	}

}
