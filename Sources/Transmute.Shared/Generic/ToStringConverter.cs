using System;

namespace Transmute
{
	public class ToStringConverter : Converter
	{
		public ToStringConverter(Type source) : base(source, typeof(string))
		{
		}

		public override object Convert(object source) => source?.ToString();
	}


	public class ToStringConverter<TSource> : ToStringConverter
	{
		public ToStringConverter() : base(typeof(TSource))
		{
		}
	}
}
