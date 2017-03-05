namespace Transmute
{
	using System;

	public class TwoWayConverter<TSource,TTarget> : ITwoWayConverter<TSource, TTarget>
	{
		public TwoWayConverter(Func<TSource, TTarget> toTarget, Func<TTarget, TSource> toSource) : this(new RelayConverter<TSource,TTarget>(toTarget), new RelayConverter<TTarget, TSource>(toSource))
		{
		}

		public TwoWayConverter(IConverter<TSource, TTarget> toTarget, IConverter<TTarget, TSource> toSource)
		{
			this.ToTarget = toTarget;
			this.ToSource = toSource;
		}

		public IConverter<TSource, TTarget> ToTarget { get; }

		public IConverter<TTarget, TSource> ToSource { get; }
	}
}
