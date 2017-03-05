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
			Assert.AreEqual(new DateTime(1970, 1, 1, 1, 0, 0, DateTimeKind.Utc), d);
		}

		[Test()]
		public void Convert_OverridingConverter_Succeed()
		{
			transmuter.Register<int, float>(x => 50.1f);
			Assert.AreEqual(50.1f, transmuter.Convert<float>(5));
		}

		[Test()]
		public void Convert_Arrays_ConvertsAllItems()
		{
			var expected = new[] { "10", "11", "12" };

			var source = new[] { 10, 11, 12 };
			var target = transmuter.Convert<string[]>(source);

			Assert.AreEqual(expected.Length, target.Length);
			Assert.AreEqual(expected[0], target[0]);
			Assert.AreEqual(expected[1], target[1]);
			Assert.AreEqual(expected[2], target[2]);
		}

		[Test()]
		public void Convert_ToString_Succeed()
		{
			Assert.AreEqual("42", transmuter.Convert<string>(42));
		}

		[Test()]
		public void Convert_RegisteringNamedConverter_Succeed()
		{
			transmuter.Register<bool, string>(x => x ? "vrai" : "faux", "toFrench");
			Assert.AreEqual("True", transmuter.Convert<string>(true));
			Assert.AreEqual("vrai", transmuter.Convert<string>(true, "toFrench"));
		}

		[Test()]
		public void Convert_TwoWay_Succeed()
		{
			transmuter.Register<int, float>(x => 50.1f);
			transmuter.Register<float, int>(x => 42);

			var twoWay = transmuter.GetTwoWayConverter<int, float>();

			Assert.AreEqual(50.1f, twoWay.ToTarget.Convert(5));
			Assert.AreEqual(42, twoWay.ToSource.Convert(5));
		}
	}
}
