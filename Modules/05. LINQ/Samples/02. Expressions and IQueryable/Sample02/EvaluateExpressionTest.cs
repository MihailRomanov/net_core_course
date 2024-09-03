using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace Sample02
{
	public class EvaluateExpressionTest
	{
		[Test]
		public void Compile()
		{
			Expression<Func<int, int>> exp1 = (a) => 1 + a;
			var compiledExp1 = exp1.Compile();
			Console.WriteLine($"Exp1: {exp1} | {compiledExp1}");
			Console.WriteLine($"{compiledExp1.Invoke(3)}");

			Expression<Action<int>> exp2 = (a) => Console.WriteLine(a);
			var compiledExp2 = exp2.Compile();
			Console.WriteLine($"Exp2 : {exp2} | {compiledExp2}");
			compiledExp2.Invoke(3);
		}
	}
}
