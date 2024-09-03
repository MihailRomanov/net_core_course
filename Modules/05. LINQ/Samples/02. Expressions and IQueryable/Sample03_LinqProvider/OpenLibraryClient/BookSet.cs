using Sample03.OpenLibraryClient.Models;
using System.Collections;
using System.Linq.Expressions;

namespace Sample03.OpenLibraryClient
{
    public class BookSet : IQueryable<Book>
    {
        public Type ElementType => typeof(Book);

        public Expression Expression { get; init; }

        public IQueryProvider Provider { get; init; }

        public BookSet()
        {
            Expression = Expression.Constant(this);
            var client = new BookSearchClient();
            Provider = new BookSearchQueryProvider(client);
        }

        public IEnumerator<Book> GetEnumerator()
        {
            return Provider.Execute<IEnumerable<Book>>(Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
