using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Sample06_RouteTemplates
{
    public class RunSamples
    {
        [TestCase("/api/One", "1")]
        [TestCase("/api/Two", "2")]
        [TestCase("/", "3")]
        public async Task CustomRouter(string url, string expectedResult)
        {
            var factory = new WebApplicationFactory<Program>();

            var httpClient = factory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "/");
            request.Headers.Add("XXX", url);
            var result = await (await httpClient.SendAsync(request))
                .Content.ReadAsStringAsync();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

    }
}
