namespace Transmute
{
	public class ChainConverter : Converter
	{
		public ChainConverter(IConverter first, IConverter second) : base(first.Source, second.Target)
		{
			this.first = first;
			this.second = second;
		}

		readonly IConverter first;

		readonly IConverter second;

		public override object Convert(object source) => second.Convert(first.Convert(source));

	}
}
