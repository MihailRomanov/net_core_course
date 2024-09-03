using System;
using System.Linq.Expressions;
using System.Linq;
using NUnit.Framework;

namespace Sample02
{
    public class ChangeExpressionTest
    {
        public class AddToIncrementTransform : ExpressionVisitor
        {
            protected override Expression VisitBinary(BinaryExpression node)
            {
                if (node.NodeType == ExpressionType.Add)
                {
                    ParameterExpression param = null;
                    ConstantExpression constant = null;
                    if (node.Left.NodeType == ExpressionType.Parameter)
                    {
                        param = (ParameterExpression)node.Left;
                    }
                    else if (node.Left.NodeType == ExpressionType.Constant)
                    {
                        constant = (ConstantExpression)node.Left;
                    }

                    if (node.Right.NodeType == ExpressionType.Parameter)
                    {
                        param = (ParameterExpression)node.Right;
                    }
                    else if (node.Right.NodeType == ExpressionType.Constant)
                    {
                        constant = (ConstantExpression)node.Right;
                    }

                    if (param != null && constant != null && constant.Type == typeof(int) && (int)constant.Value == 1)
                    {
                        return Expression.Increment(param);
                    }

                }

                return base.VisitBinary(node);
            }
        }

        [Test]
        public void AddToIncrementTransformTest()
        {
            Expression<Func<int, int>> source_exp = (a) => a + (a + 1) * (a + 5) * (a + 1);
            var result_exp = (new AddToIncrementTransform().VisitAndConvert(source_exp, ""));


            Console.WriteLine(source_exp + " " + source_exp.Compile().Invoke(3));
            Console.WriteLine(result_exp + " " + result_exp.Compile().Invoke(3));

        }


        public class StringFormatTransform : ExpressionVisitor
        {
            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                if (node.Method.DeclaringType == typeof(string) &&
                    node.Method.Name == "Format" &&
                    node.Arguments.All(a =>
                            a.NodeType == ExpressionType.Constant ||
                            a.NodeType == ExpressionType.Convert &&
                                ((UnaryExpression)a).Operand.NodeType == ExpressionType.Constant
                    ))
                    return Expression.Constant(Expression.Lambda<Func<string>>(node).Compile().Invoke());

                return base.VisitMethodCall(node);
            }
        }


        [Test]
        public void StringFormatTransformTest()
        {
            Expression<Func<string>> source_exp = () => "3" + "5" + String.Format("String {0} : {1}", 1, "s1");
            var result_exp = (new StringFormatTransform().VisitAndConvert(source_exp, ""));

            Console.WriteLine(source_exp + " | " + source_exp.Compile().Invoke());
            Console.WriteLine(result_exp + " | " + result_exp.Compile().Invoke());
        }
    }
}
