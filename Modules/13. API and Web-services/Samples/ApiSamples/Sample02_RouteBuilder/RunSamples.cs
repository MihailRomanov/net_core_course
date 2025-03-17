using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Sample02_RouteBuilder
{
    public class RunSamples
    {
        [TestCase("/api/one", "1")]
        [TestCase("/api/two", "2")]
        [TestCase("/", "3")]
        public async Task MapRouter(string url, string expectedResult)
        {
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();

            var result = await client.GetStringAsync(url);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
