using System.Collections;
using System.Linq.Expressions;

namespace Sample03.OpenLibraryClient
{
    internal class BookSearchQueryProvider : IQueryProvider
    {
        private readonly BookSearchClient bookSearchClient;

        public BookSearchQueryProvider(BookSearchClient bookSearchClient)
        {
            this.bookSearchClient = bookSearchClient;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            var elemetType = TypeHelper.GetElementType(expression.Type);
            var queryType = typeof(InternalQuery<>).MakeGenericType(elemetType);
            return (IQueryable)Activator.CreateInstance(queryType, expression, this)!;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new InternalQuery<TElement>(expression, this);
        }

        public object? Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var translator = new ExpressionToBookSearchRequestTranslator();
            var request = translator.Translate(expression);

            var response = bookSearchClient.Search(request);
            IEnumerable objects = response.Books;

            return (TResult)(objects);
        }
    }
}
