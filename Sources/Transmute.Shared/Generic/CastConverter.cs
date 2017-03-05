
namespace Transmute
{
	public class CastConverter<TSource,TTarget> : Converter<TSource,TTarget>
	{
		public override TTarget Convert(TSource source) => (TTarget)System.Convert.ChangeType(source, typeof(TTarget));
	}
}
