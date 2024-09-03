using System.Collections;
using System.Linq.Expressions;

namespace Sample03.OpenLibraryClient
{
    internal class InternalQuery<T> : IOrderedQueryable<T>
    {
        public Type ElementType => typeof(T);

        public Expression Expression { get; init; }

        public IQueryProvider Provider { get; init; }

        public InternalQuery(Expression expression, IQueryProvider provider)
        {
            Expression = expression;
            Provider = provider;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Provider.Execute<IEnumerable<T>>(Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
