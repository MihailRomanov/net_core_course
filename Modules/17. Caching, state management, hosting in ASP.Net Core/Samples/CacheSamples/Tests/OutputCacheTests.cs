using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Tests
{
    public class OutputCacheTests
    {
        [Test]
        public async Task VerifyByDate()
        {
            var factory = new WebApplicationFactory<WebApp3.Program>();

            var client = factory.CreateDefaultClient();

            var request1 = new HttpRequestMessage(HttpMethod.Get, "/");
            var response1 = await client.SendAsync(request1);

            var lastModified = response1.Content.Headers.LastModified;

            var request2 = new HttpRequestMessage(HttpMethod.Get, "/");
            request2.Headers.IfModifiedSince = lastModified;

            var response2 = await client.SendAsync(request2);

            Assert.That(response1.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response2.StatusCode, Is.EqualTo(HttpStatusCode.NotModified));
        }

        [Test]
        public async Task ReVerifyByDateAndCacheResponse()
        {
            var factory = new WebApplicationFactory<WebApp3.Program>();

            var client = factory.CreateDefaultClient(new RevalidateDelegate());

            var result1 = await client.GetStringAsync("/");
            var result2 = await client.GetStringAsync("/");

            Assert.That(result1, Is.EqualTo(result2));
        }

    }
}
