namespace Transmute
{
	public class TypedConverter<TSource,TTarget> : Converter, IConverter<TSource, TTarget>
	{
		public TypedConverter(IConverter notTyped) : base(typeof(TSource),typeof(TTarget))
		{
			this.notTyped = notTyped;
		}

		private IConverter notTyped;

		public override object Convert(object source) => notTyped.Convert(source);

		public TTarget Convert(TSource source) => (TTarget)notTyped.Convert(source);

	}
}
