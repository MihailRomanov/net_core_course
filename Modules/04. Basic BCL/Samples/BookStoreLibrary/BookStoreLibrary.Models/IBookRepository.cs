namespace BookStoreLibrary.Models
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
    }
}