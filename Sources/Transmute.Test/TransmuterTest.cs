namespace Transmute.Test
{
	using NUnit.Framework;
	using System;

	[TestFixture()]
	public class Test
	{
		private Transmuter transmuter;

		[SetUp]
		public void Setup() => transmuter = new Transmuter();

		[Test()]
		public void Convert_IntToFloat_Succeed()
		{
			Assert.AreEqual(transmuter.Convert<float>(5), 5.0f);
		}

		[Test()]
		public void Convert_IntToBool_Succeed()
		{
			Assert.IsTrue(transmuter.Convert<bool>(5));
			Assert.IsFalse(transmuter.Convert<bool>(0));
		}

		[Test()]
		public void Convert_Composability_Succeed()
		{
			// int -> long -> DateTime
			var d = transmuter.Convert<DateTime>(0);
			Assert.AreEqual(d,  new DateTime(1970,1,1,1,0,0,DateTimeKind.Utc));
		}

		[Test()]
		public void Convert_OverridingConverter_Succeed()
		{
			transmuter.Register<int, float>(x => 50.1f);
			Assert.AreEqual(transmuter.Convert<float>(5), 50.1f);
		}
	}
}
