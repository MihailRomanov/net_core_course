using BookStoreLibrary.Models;
using BookStoreLibrary.Tests;
using System.IO.Abstractions;

namespace BookStoreLibrary.Implementation
{
    public class SevicesFactory : IServicesFactory
    {
        public IBookService GetBookService(string storeFile)
        {
            return new BookService(new BookRepository(storeFile, new FileSystem()));
        }
    }
}
