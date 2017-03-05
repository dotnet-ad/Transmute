namespace Transmute
{
	using System;
	using System.Linq;
	using System.Collections.Generic;

	public partial class Transmuter
	{
		public Transmuter()
		{
			this.RegisterBase();
			this.RegisterPlatform();
		}

		private List<IConverter> converters = new List<IConverter>();

		public Transmuter Register(IConverter  converter)
		{
			var existing = converters.FindIndex(x => x.HasSameTypes(converter));
			if (existing >= 0) this.converters.RemoveAt(existing);
			this.converters.Add(converter);
			return this;
		}

		public Transmuter Register<TSource,TTarget>(Func<TSource,TTarget> convert)
		{
			return this.Register(new RelayConverter<TSource,TTarget>(convert));
		}

		private KeyValuePair<bool,IConverter> FindConverter(Type source, Type target)
		{
			var targets = this.converters.Where(x => x.Target == target);

			if (targets.Any())
			{
				var converter = targets.FirstOrDefault(x => x.Source == source);

				if (converter != null)
					return new KeyValuePair<bool, IConverter>(false, converter);

				foreach (var item in targets)
				{
					converter = FindConverter(source, item.Source).Value;
					if (converter != null)
						return new KeyValuePair<bool, IConverter>(true, new ChainConverter(converter, item));
				}
			}

			return new KeyValuePair<bool, IConverter>(false, null);
		}

		public IConverter GetConverter(Type source, Type target)
		{
			var result = this.FindConverter(source, target);

			if (result.Key)
				this.Register(result.Value);
			
			return result.Value;
		}

		public IConverter<TSource,TTarget> GetConverter<TSource,TTarget>()
		{
			var converter = this.GetConverter(typeof(TSource), typeof(TTarget));
			return converter as IConverter<TSource, TTarget> ?? new TypedConverter<TSource, TTarget>(converter);
		}

		public TTarget Convert<TTarget>(object source) => (TTarget)this.Convert(source, typeof(TTarget));

		public object Convert(object source, Type target)
		{
			var converter = this.GetConverter(source.GetType(), target);
			return converter.Convert(source);
		}
	}
}
