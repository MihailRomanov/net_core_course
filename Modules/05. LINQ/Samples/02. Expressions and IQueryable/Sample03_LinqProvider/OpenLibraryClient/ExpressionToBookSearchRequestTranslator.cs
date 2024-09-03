using System.Linq.Expressions;
using System.Text;

namespace Sample03.OpenLibraryClient
{

    internal class ExpressionToBookSearchRequestTranslator : ExpressionVisitor
    {
        private string query = "";
        public BookSearchRequest Translate(Expression expression)
        {
            Visit(expression);

            return new BookSearchRequest(query);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable))
            {
                switch (node.Method.Name)
                {
                    case "Where":
                        var processor = new WhereProcessor();
                        processor.Visit(node.Arguments[1]);
                        query = processor.QueryString;

                        break;
                    case "Take":
                        break;
                    case "Skip":
                        break;
                    default:
                        throw new InvalidOperationException(
                            $"Unsupported method {node.Method.Name}");
                }
            }

            return node;
        }


        private class WhereProcessor : ExpressionVisitor
        {
            readonly StringBuilder resultString = new();

            public string QueryString => resultString.ToString();

            protected override Expression VisitBinary(BinaryExpression node)
            {
                switch (node.NodeType)
                {
                    case ExpressionType.Equal:
                        if (node.Left.NodeType != ExpressionType.MemberAccess)
                            throw new NotSupportedException("Left operand should be property or field");

                        if (node.Right.NodeType != ExpressionType.Constant)
                            throw new NotSupportedException("Right operand should be constant");

                        Visit(node.Left);
                        resultString.Append("(");
                        Visit(node.Right);
                        resultString.Append(")");
                        break;

                    default:
                        throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
                }

                return node;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                resultString.Append(node.Member.Name).Append(":");

                return base.VisitMember(node);
            }

            protected override Expression VisitConstant(ConstantExpression node)
            {
                resultString.Append(node.Value);

                return node;
            }
        }

    }
}
