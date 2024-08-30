using BookStoreLibrary.Models;
using FluentAssertions;
using Moq;

namespace BookStoreLibrary.Tests
{
    public class BookServiceTests
    {
        List<Book> books = new List<Book>
            {
                new Book("Преступление и наказание", 2000),
                new Book("Война и мир", 1956),
                new Book("Буратино", 2005)
            };

        [Test]
        public void GetAll_ReturnAllBookList()
        {
            Mock<IBookRepository> repositoryMock = GetBookRepositoryMock();

            IBookService bookService = new BookService(repositoryMock.Object);
            IEnumerable<Book> result = bookService.GetAll();

            result.Should().Equal(books);
        }

        private Mock<IBookRepository> GetBookRepositoryMock()
        {
            var repositoryMock = new Mock<IBookRepository>();
            repositoryMock.Setup(repos => repos.GetAll()).Returns(books);
            return repositoryMock;
        }

        [Test]
        [TestCase(OrderBy.Asc)]
        [TestCase(OrderBy.Desc)]
        public void GetAllSorted_ReturnAllListInAscOrDescOrderByYear(OrderBy orderBy)
        {
            IBookService bookService = 
                new BookService(GetBookRepositoryMock().Object);
            IEnumerable<Book> result = bookService.GetAll(orderBy);

            result.Should().Equal(orderBy == OrderBy.Asc
                ? books.OrderBy(b => b.Year)
                : books.OrderByDescending(b => b.Year));
        }
    }
}
