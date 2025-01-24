using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using SimpleServer;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;

namespace TestClient
{
    public class ServerProcessingTests
    {
        WebApplicationFactory<Program> factory;

        [SetUp]
        public void Setup()
        {
            factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => builder.ConfigureLogging(l => l.ClearProviders()));
        }

        [TearDown]
        public void TearDown()
        {
            factory.Dispose();
        }

        [Test]
        public async Task SendForm()
        {
            var client = factory.CreateClient();

            var formData = new Dictionary<string, string>
            {
                ["login"] = "mihail_r@somesite.ru",
                ["password"] = "123",
                ["remember_me"] = ""
            };

            var result = await client.PostAsync("/form", new FormUrlEncodedContent(formData));
            Console.WriteLine($"{result.StatusCode} \n {await result.Content.ReadAsStringAsync()}");
        }

        const string fileContent1 = "File 1";
        const string fileContent2 = "File 2 !!!!!!!!!!!";

        [Test]
        public async Task UploadFiles()
        {
            var client = factory.CreateClient();

            var file1 = new MemoryStream(Encoding.UTF8.GetBytes(fileContent1));
            var file2 = new MemoryStream(Encoding.UTF8.GetBytes(fileContent2));

            var content1 = new StreamContent(file1);
            content1.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Text.Plain);

            var content2 = new StreamContent(file2);
            content2.Headers.ContentType = new MediaTypeHeaderValue("pdf/application");

            var content = new MultipartFormDataContent
            {
                { new StringContent("New document"), "doc_name" },
                { new StringContent("2341"), "doc_id" },
                { content1, "file1", "file1.txt" },
                { content2, "file2", "file2.txt" }
            };

            var result = await client.PostAsync("/form_with_file", content);
            Console.WriteLine($"{result.StatusCode} \n {await result.Content.ReadAsStringAsync()}");
        }

        [Test]
        public async Task ReadWriteCookies()
        {
            var client = factory.CreateClient();

            for (int i = 0; i < 10; i++)
            {
                await client.GetStringAsync("/cookie_counter");
            }
        }

        [Test]
        public async Task SendObject()
        {
            var client = factory.CreateClient();

            var dto = new TestDto(1, "dddd");

            for (int i = 0; i < 10; i++)
            {
                var result = await client.PostAsJsonAsync("/object", dto);

                var newDto = await result.Content.ReadFromJsonAsync<TestDto>();
             
                Console.WriteLine(newDto);

                dto = newDto;
            }
        }
    }
}
