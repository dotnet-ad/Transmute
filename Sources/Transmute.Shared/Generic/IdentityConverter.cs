namespace Transmute
{
	using System;

	namespace Transmute
	{
		public class IdentityConverter : Converter
		{
			public IdentityConverter(Type source) : base(source, source) {}

			public override object Convert(object source) => source;
		}
	}

}
