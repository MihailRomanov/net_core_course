using BookStoreLibrary.Models;

namespace BookStoreLibrary.Tests
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public IEnumerable<Book> GetAll(OrderBy? orderByYear = null)
        {
            return orderByYear switch
            {
                OrderBy.Asc => bookRepository.GetAll().OrderBy(b => b.Year),
                OrderBy.Desc => bookRepository.GetAll().OrderByDescending(b => b.Year),
                _ => bookRepository.GetAll(),
            };
        }
    }
}