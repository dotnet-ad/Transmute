namespace Transmute
{
	using Android.Views;

	public static class ViewStatesConverters
	{
		public static void Register(Transmuter transmuter)
		{
			transmuter.Register(FromBool());
			transmuter.Register(ToBool());
		}

		#region bool

		public static IConverter<bool, ViewStates> FromBool() => new RelayConverter<bool, ViewStates>((value) =>
		{
			return value ? ViewStates.Visible : ViewStates.Gone;
		});

		public static IConverter<ViewStates, bool> ToBool() => new RelayConverter<ViewStates, bool>((value) =>
		{
			return value == ViewStates.Visible;
		});

		#endregion
	}

}
