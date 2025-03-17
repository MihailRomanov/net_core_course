using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using Sample03_MVCRouter;

namespace Sample00
{
    public class RunSamples
    {
        [TestCase("/api/one", "1")]
        [TestCase("/api/two", "2")]
        [TestCase("/", "3")]
        [TestCase("/api/f", "ff")]
        public async Task MapRouter(string url, string expectedResult)
        {
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();

            var result = await client.GetStringAsync(url);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
