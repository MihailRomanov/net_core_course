namespace BookStoreLibrary.Models
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll(OrderBy? orderByYear = null);
    }
}