using BookStoreLibrary.Implementation;
using BookStoreLibrary.Models;
using FluentAssertions;
using System.IO.Abstractions.TestingHelpers;

namespace BookStoreLibrary.Tests
{
    public class BookRepositoryTests
    {
        List<Book> books = new List<Book>
            {
                new Book("Преступление и наказание", 2000),
                new Book("Война и мир", 1956),
                new Book("Буратино", 2005)
            };

        string bookStoreFileContent = """
            Преступление и наказание|||2000
            Война и мир|||1956
            Буратино|||2005
            """;

        const string bookStoreFileName = "book.txt";

        [Test]
        public void GetAll_ReturnAllLoadedBooks()
        {
            var fileSystem = new MockFileSystem();
            fileSystem.AddFile(bookStoreFileName, new MockFileData(bookStoreFileContent));


            IBookRepository bookRepository = 
                new BookRepository(bookStoreFileName, fileSystem);

            var result = bookRepository.GetAll();

            result.Should().Equal(books);
        }
    }
}
