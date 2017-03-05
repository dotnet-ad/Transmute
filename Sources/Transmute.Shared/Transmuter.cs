using Transmute.Transmute;

namespace Transmute
{
	using System;
	using System.Collections.Generic;

	public partial class Transmuter
	{
		#region Default instance

		public static Lazy<Transmuter> instance = new Lazy<Transmuter>(() => new Transmuter());

		public static Transmuter Default => instance.Value;

		#endregion

		public Transmuter()
		{
			this.RegisterBase();
			this.RegisterPlatform();
		}

		private Dictionary<Type, Dictionary<Tuple<Type,string>,IConverter>> converters = new Dictionary<Type, Dictionary<Tuple<Type, string>, IConverter>>();

		public Transmuter Register(IConverter converter, string name = null)
		{
			Dictionary<Tuple<Type, string>, IConverter> targets;

			if(!converters.TryGetValue(converter.Target, out targets))
			{
				targets = new Dictionary<Tuple<Type, string>, IConverter>();
				converters[converter.Target] = targets;
			}


			var key = new Tuple<Type, string>(converter.Source, name);
			targets[key] = converter;

			return this; 
		}

		public Transmuter Register<TSource,TTarget>(Func<TSource,TTarget> convert, string name = null)
		{
			return this.Register(new RelayConverter<TSource,TTarget>(convert), name);
		}

		private KeyValuePair<bool,IConverter> FindConverter(Type source, Type target, string name = null)
		{
			if(source == target && name == null)
			{
				return new KeyValuePair<bool, IConverter>(true, new IdentityConverter(source));
			}

			Dictionary<Tuple<Type, string>, IConverter> targets;

			if (converters.TryGetValue(target, out targets))
			{
				var key = new Tuple<Type, string>(source, name); 

				IConverter converter;

				// Referenced converter
				if (targets.TryGetValue(key, out converter))
				{
					return new KeyValuePair<bool, IConverter>(false, converter);
				}

				// Composability
				foreach (var item in targets)
				{
					converter = FindConverter(source, item.Key.Item1, name).Value;
					if (converter != null)
						return new KeyValuePair<bool, IConverter>(true, new ChainConverter(converter, item.Value));
				}
			}

			// Arrays
			if(source.IsArray && target.IsArray) // TODO same for all collections
			{
				var itemConverter = this.GetConverter(source.GetElementType(), target.GetElementType());
				if(itemConverter != null)
				{
					return new KeyValuePair<bool, IConverter>(true, new ArrayConverter(itemConverter));
				}
				return new KeyValuePair<bool, IConverter>(false, null);
			}

			return new KeyValuePair<bool, IConverter>(false, null);
		}

		public IConverter GetConverter(Type source, Type target, string name = null)
		{
			var result = this.FindConverter(source, target, name);

			if (result.Value == null)
			{
				if (source == target && name == null)
				{
					result = new KeyValuePair<bool, IConverter>(true, new IdentityConverter(source));
				}
				else if (target == typeof(string))
				{
					result = new KeyValuePair<bool, IConverter>(true, new ToStringConverter(source));
				}
				else throw new ArgumentException($"No registration for converting values from {source} to {target}");
			}

			if (result.Key)
				this.Register(result.Value, name);

			return result.Value;
		}

		public IConverter<TSource,TTarget> GetConverter<TSource,TTarget>(string name = null)
		{
			var converter = this.GetConverter(typeof(TSource), typeof(TTarget), name);
			return converter as IConverter<TSource, TTarget> ?? new TypedConverter<TSource, TTarget>(converter);
		}

		public ITwoWayConverter<TSource, TTarget> GetTwoWayConverter<TSource, TTarget>(string name = null)
		{
			var forward = this.GetConverter<TSource, TTarget>(name);
			var back = this.GetConverter<TTarget, TSource>(name);
			return new TwoWayConverter<TSource, TTarget>(forward, back);
		}

		public TTarget Convert<TTarget>(object source, string name = null) => (TTarget)this.Convert(source, typeof(TTarget), name);

		public object Convert(object source, Type target, string name = null)
		{
			var converter = this.GetConverter(source.GetType(), target, name);
			return converter.Convert(source);
		}
	}
}
