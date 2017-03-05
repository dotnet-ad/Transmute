namespace Transmute
{
	using System;

	public class RelayConverter<TSource, TTarget> : Converter<TSource, TTarget>
	{
		public RelayConverter(Func<TSource,TTarget> convert)
		{
			this.convert = convert;
		}

		private Func<TSource, TTarget> convert;

		public override TTarget Convert(TSource source) => this.convert(source);

	}
}
