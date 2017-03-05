namespace Transmute
{
	public interface ITwoWayConverter<TSource,TTarget>
	{
		IConverter<TSource, TTarget> ToTarget { get; }

		IConverter<TTarget, TSource> ToSource { get; }
	}
}
