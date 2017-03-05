namespace Transmute
{
	public static class ITwoWayConverterExtensions
	{
		public static ITwoWayConverter<TSource, TTarget> Chain<TSource, TBy, TTarget>(this ITwoWayConverter<TSource, TBy> first, ITwoWayConverter<TBy, TTarget> second)
		{
			return new TwoWayConverter<TSource, TTarget>(first.ToTarget.Chain(second.ToTarget), second.ToSource.Chain(first.ToSource));
		}
	}
}
