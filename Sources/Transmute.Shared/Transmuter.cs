namespace Transmute
{
	using System;
	using System.Collections.Generic;

	public partial class Transmuter
	{
		public Transmuter()
		{
			this.RegisterBase();
			this.RegisterPlatform();
		}

		private Dictionary<Type,Dictionary<Type,IConverter>> converters = new Dictionary<Type, Dictionary<Type, IConverter>>();

		public Transmuter Register(IConverter  converter)
		{
			Dictionary<Type, IConverter> fromTarget;

			if(!converters.TryGetValue(converter.Target, out fromTarget))
			{
				fromTarget = new Dictionary<Type, IConverter>();
				converters[converter.Target] = fromTarget;
			}

			fromTarget[converter.Source] = converter;

			return this;
		}

		public Transmuter Register<TSource,TTarget>(Func<TSource,TTarget> convert)
		{
			return this.Register(new RelayConverter<TSource,TTarget>(convert));
		}

		private KeyValuePair<bool,IConverter> FindConverter(Type source, Type target)
		{
			Dictionary<Type, IConverter> targets;

			if (converters.TryGetValue(target, out targets))
			{
				IConverter converter;

				if(targets.TryGetValue(source, out converter))
				{
					return new KeyValuePair<bool, IConverter>(false, converter);
				}

				foreach (var item in targets)
				{
					converter = FindConverter(source, item.Key).Value;
					if (converter != null)
						return new KeyValuePair<bool, IConverter>(true, new ChainConverter(converter, item.Value));
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
