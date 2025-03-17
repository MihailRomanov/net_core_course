using ClientLibrary.Models;
using System.Net.Http.Json;

namespace ClientLibrary
{
    public class CategoryClient : ICategoryClient
    {
        private readonly HttpClient httpClient;

        public CategoryClient(string baseUrl) 
            => httpClient = new HttpClient() { BaseAddress = new Uri(baseUrl) };

        public CategoryClient(HttpClient httpClient) 
            => this.httpClient = httpClient;

        public async Task<Category?> Get(int id)
        {
            return await httpClient
                .GetFromJsonAsync<Category>($"/categories/{id}");
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await httpClient
                .GetFromJsonAsync<List<Category>>($"/categories") 
                ?? Enumerable.Empty<Category>();
        }

        public async Task<byte[]> GetPicture(int id)
        {
            return await httpClient.GetByteArrayAsync($"/categories/{id}/image");
        }

        public Task Update(Category category)
        {
            return httpClient.PutAsJsonAsync("/categories", category);
        }
    }
}
