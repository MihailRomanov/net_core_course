using Microsoft.AspNetCore.Mvc.Testing;

namespace ClientLibrary.Tests
{
    public class CategoriesClientTests
    {
        [Test]
        public async Task GetAllCategories()
        {
            ICategoryClient client = new CategoryClient("http://localhost:5000/");

            var categoies = (await client.GetAll()).ToList();

            Console.WriteLine(categoies.Count());
            categoies.ForEach(c => Console.WriteLine(c.CategoryName));
        }

        [Test]
        public async Task GetAllCategories2()
        {
            var factory = new WebApplicationFactory<WebApplication1.Program>();
            var httpClient = factory.CreateClient();


            ICategoryClient client = new CategoryClient(httpClient);

            var categoies = (await client.GetAll()).ToList();

            Console.WriteLine(categoies.Count());
            categoies.ForEach(c => Console.WriteLine(c.CategoryName));
        }

        [Test]
        public async Task UpdateCategory()
        {
            var client = new CategoryClient("http://localhost:5000/");

            var category = (await client.Get(1))!;

            var oldName = category.CategoryName;
            category.CategoryName += "1";

            await client.Update(category);

            var category2 = (await client.Get(1))!;
            Assert.That(category2.CategoryName, Is.EqualTo(oldName += "1"));
        }
    }
}
