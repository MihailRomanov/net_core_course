using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace Sample02
{
	public class CreateExpressionTest
	{
		[Test]
		public void LambdaVsExpression()
		{
			Func<int, int> lambda = (a) => 1 + a;
			Expression<Func<int, int>> expression = (a) => 1 + a;

			Console.WriteLine(lambda);
			Console.WriteLine(expression);
		}

		[Test]
		public void FromLambda()
		{
			Expression<Func<int>> exp1 = () => 1 + 3 * 5;
			Expression<Func<int, double>> exp2 = (x) => (int)(Math.PI * x);
			Expression<Func<object>> exp3 = () => new { Name = "Alex", Age = 17 };

			Console.WriteLine(exp1);
			Console.WriteLine(exp2);
			Console.WriteLine(exp3);
		}

		[Test]
		public void Manualy()
		{
			var exp1 = Expression.Lambda(
				Expression.Add(
					Expression.Constant(1),
					Expression.Multiply(
							Expression.Constant(3),
							Expression.Constant(5)
							)
					)
				);

			var xparam = Expression.Parameter(typeof(double), "x");

			var exp2 = Expression.Lambda(
				Expression.Convert(
					Expression.Multiply(
						Expression.Constant(Math.PI),
						xparam
						),
					typeof(int)),
				xparam);

			var obj = new { Name = "Alex", Age = 17 };

			var exp3 = Expression.Lambda(
				Expression.New(
					obj.GetType().GetConstructors()[0],
					Expression.Constant("Alex"), Expression.Constant(17)
					)
				);

			Console.WriteLine(exp1);
			Console.WriteLine(exp2);
			Console.WriteLine(exp3);
		}
	}
}
