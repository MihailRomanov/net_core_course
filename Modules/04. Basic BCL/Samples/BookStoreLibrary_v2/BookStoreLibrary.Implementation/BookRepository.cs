using BookStoreLibrary.Models;
using System.IO.Abstractions;

namespace BookStoreLibrary.Implementation
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(string bookStoreFileName, IFileSystem fileSystem)
            : base(bookStoreFileName, fileSystem)
        {
        }

        public IEnumerable<Book> GetAll()
        {
            return Load();
        }

        protected override Book ToEntity(string line)
        {
            var parts = line.Split(delim);
            return new Book(parts[0], uint.Parse(parts[1]));
        }
    }
}
