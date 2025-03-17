using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Sample11_EndpointMetadata
{
    public class RunSamples
    {
        [TestCase("/?p1=111&p2=456", "111 - 456")]
        [TestCase("/api?p1=111&p2=456", "111 - 456")]
        public async Task Sample01(string url, string expectedResult)
        {
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();

            var result = await client.GetStringAsync(url);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("/?p1=111")]
        [TestCase("/?p2=456")]
        [TestCase("/api?p1=111")]
        [TestCase("/api?p2=456")]
        public async Task Sample02(string url)
        {
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();

            var result = await client.SendAsync(
                new HttpRequestMessage() { RequestUri = new Uri(url, UriKind.Relative) });
            Assert.That(result.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
        }

    }
}
