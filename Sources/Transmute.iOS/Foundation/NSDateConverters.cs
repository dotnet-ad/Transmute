namespace Transmute
{
	using System;
	using Foundation;

	public static class NSDateConverters
	{
		public static void Register(Transmuter transmuter)
		{
			transmuter.Register(FromDateTime());
			transmuter.Register(ToDateTime());
		}

		private static DateTime Reference = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

		#region int

		public static IConverter<DateTime, NSDate> FromDateTime() => new RelayConverter<DateTime, NSDate>((value) =>
		 {
			 var utcDateTime = value.ToUniversalTime();
			 var date = NSDate.FromTimeIntervalSinceReferenceDate((utcDateTime - Reference).TotalSeconds);
			 return date;
		 });

		public static IConverter<NSDate, DateTime> ToDateTime() => new RelayConverter<NSDate, DateTime>((value) =>
		  {
			  var utcDateTime = Reference.AddSeconds(value.SecondsSinceReferenceDate);
			  var dateTime = utcDateTime.ToLocalTime();
			  return dateTime;
		  });

		#endregion
	}
}
