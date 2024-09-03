using NUnit.Framework;
using System;
using System.Linq;

namespace Sample01
{
	public class MyEnumerableTest
	{
		int[] source = Enumerable.Range(1, 20).ToArray();

		[Test]
		public void SimpleEveryTest()
		{
			foreach (var step in Enumerable.Range(1, 5))
			{
				Console.Write("Step {0} :", step);
				foreach (var i in source.Every(step))
				{
					Console.Write("{0} ", i);
				}

				Console.Write("\n\n");
			}
		}

		[Test]
		public void EveryWithStepFunctionTest()
		{
			foreach (var i in source.Every(x => 1))
			{
				Console.Write("{0} ", i);
			}

			Console.Write("\n\n");


			foreach (var i in source.Every(x => x))
			{
				Console.Write("{0} ", i);
			}

			Console.Write("\n\n");

			var step = 1;

			foreach (var i in source.Every(x => step++))
			{
				Console.Write("{0} ", i);
			}

			Console.Write("\n\n");

		}

		[Test]
		public void MyWhereTest()
		{
			int[] source = Enumerable.Range(1, 20).ToArray();

			foreach (var i in source.MyWhere(x => x % 2 == 0).Select(x => x + 1))
			{
				Console.Write("{0} ", i);
			}

			Console.Write("\n\n");
		}
	}
}
