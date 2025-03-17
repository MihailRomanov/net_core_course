using ClientLibrary.Models;

namespace ClientLibrary
{
    public interface ICategoryClient
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category?> Get(int id);
        Task<byte[]> GetPicture(int id);
        Task Update(Category category);
    }
}
