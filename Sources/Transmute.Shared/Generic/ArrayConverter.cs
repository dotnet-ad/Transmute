namespace Transmute
{
	using System;

	public class ArrayConverter : Converter
	{
		public ArrayConverter(IConverter itemConverter) : base(itemConverter.Source.MakeArrayType(), itemConverter.Target.MakeArrayType())
		{
			this.itemConverter = itemConverter;
		}

		private IConverter itemConverter;

		public override object Convert(object source)
		{
			var sourceArray = source as Array;
			var targetArray = Array.CreateInstance(this.itemConverter.Target, sourceArray.Length);

			for (int i = 0; i < sourceArray.Length; i++)
			{
				var sourceItem = sourceArray.GetValue(i);
				var targetItem = itemConverter.Convert(sourceItem);
				targetArray.SetValue(targetItem, i);
			}

			return targetArray;
		}
	}
}
