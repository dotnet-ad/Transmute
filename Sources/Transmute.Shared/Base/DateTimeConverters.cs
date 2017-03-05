namespace Transmute
{
	using System;
	using System.Linq;

	public static class DateTimeConverters
	{
		public static void Register(Transmuter transmuter)
		{
			transmuter.Register(FromTimestamp());
			transmuter.Register(ToTimestamp());
		}

		#region timestamp

		public static IConverter<long, DateTime> FromTimestamp() => new RelayConverter<long, DateTime>((value) =>
		 {
			 return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(value).ToLocalTime();
		 });

		public static IConverter<DateTime, long> ToTimestamp() => new RelayConverter<DateTime, long>((value) =>
		 {
			 var ts = (value.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
			 return (long)ts.TotalMilliseconds;
		 });

		#endregion

	}
}
