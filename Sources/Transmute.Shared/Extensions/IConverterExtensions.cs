namespace Transmute
{
	public static class IConverterExtensions
	{
		public static IConverter<TSource,TTarget> Chain<TSource,TBy,TTarget>(this IConverter<TSource, TBy> first, IConverter<TBy,TTarget> second)
		{
			return new TypedConverter<TSource, TTarget>(new ChainConverter(first, second));
		}
	}
}
