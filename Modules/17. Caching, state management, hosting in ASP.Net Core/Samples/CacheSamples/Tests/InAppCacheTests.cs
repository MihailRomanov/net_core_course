using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Tests
{
    public class InAppCacheTests
    {
        [Test]
        public async Task Memory()
        {
            var factory = new WebApplicationFactory<WebApp1.Program>();

            var client = factory.CreateClient();
            var r1 = await client.GetStringAsync("/memory/a");
            var r2 = await client.GetStringAsync("/memory/a");
            var r3 = await client.GetStringAsync("/memory/b");

            Assert.That(r1, Is.EqualTo(r2));
            Assert.That(r1, Is.Not.EqualTo(r3));
        }


        [Test]
        public async Task Memory2Instance()
        {
            var factory1 = new WebApplicationFactory<WebApp1.Program>();
            var factory2 = new WebApplicationFactory<WebApp1.Program>();

            var client1 = factory1.CreateClient();
            var client2 = factory2.CreateClient();
            var r1 = await client1.GetStringAsync("/memory/a");
            var r2 = await client2.GetStringAsync("/memory/a");
            
            Assert.That(r1, Is.Not.EqualTo(r2));
        }

        [Test]
        public async Task Distributed2Instance()
        {
            var factory1 = new WebApplicationFactory<WebApp1.Program>();
            var factory2 = new WebApplicationFactory<WebApp1.Program>();

            var client1 = factory1.CreateClient();
            var client2 = factory2.CreateClient();
            var r1 = await client1.GetStringAsync("/dist/a");
            var r2 = await client2.GetStringAsync("/dist/a");

            Assert.That(r1, Is.EqualTo(r2));  
        }

        [Test]
        public async Task MemoryAndWatchFile()
        {
            var factory = new WebApplicationFactory<WebApp1.Program>();

            var env = factory.Services.GetRequiredService<IHostEnvironment>();

            var fileName = Path.Combine(env.ContentRootPath, "dir", "file.txt");
            File.WriteAllText(fileName, "w");

            var client = factory.CreateClient();
            var r1 = await client.GetStringAsync("/memory/file");

            File.WriteAllText(fileName, "www");
            await Task.Delay(TimeSpan.FromSeconds(6));

            var r2 = await client.GetStringAsync("/memory/file");

            Assert.That(r1, Is.EqualTo("w"));
            Assert.That(r2, Is.EqualTo("www"));
        }
    }
}
