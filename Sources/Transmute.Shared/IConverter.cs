namespace Transmute
{
	using System;

	public interface IConverter
	{
		Type Source { get; }

		Type Target { get; }

		object Convert(object source);

		bool HasSameTypes(IConverter other);
	}

	public interface IConverter<TSource,TTarget> : IConverter
	{
		TTarget Convert(TSource source);
	}
}
