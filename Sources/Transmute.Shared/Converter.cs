namespace Transmute
{
	using System;

	public abstract class Converter : IConverter
	{
		public Converter(Type source, Type target)
		{
			this.Target = target;
			this.Source = source;
		}

		public Type Source { get; }

		public Type Target { get; }

		public abstract object Convert(object source);

		public bool HasSameTypes(IConverter other) => (this.Source == other.Source && this.Target == other.Target);
	}

	public abstract class Converter<TSource, TTarget>  : Converter, IConverter<TSource, TTarget>
	{
		public Converter() : base(typeof(TSource),typeof(TTarget))
		{

		}

		public override object Convert(object source) => this.Convert((TSource)source);

		public abstract TTarget Convert(TSource source);
	}
}
