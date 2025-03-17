using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Tests
{
    public class ResponseCacheTests
    {
        [TestCase("/")]
        [TestCase("/api/values")]
        public async Task CacheOnServer(string url)
        {
            var factory = new WebApplicationFactory<WebApp2.Program>();

            var client = factory.CreateDefaultClient();
            
            var response1 = await client.GetStringAsync(url);
            var response2 = await client.GetStringAsync(url);
            await Task.Delay(10000);
            var response3 = await client.GetStringAsync(url);
            var rresponse4 = await client.GetStringAsync(url);

            Assert.That(response1, Is.EqualTo(response2));
            Assert.That(response3, Is.EqualTo(rresponse4));
            Assert.That(response1, Is.Not.EqualTo(response3));
        }

        [Test]
        public async Task RevalidateByLastModified()
        {
            var factory = new WebApplicationFactory<WebApp2.Program>();

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
        public async Task RevalidateByLastModifiedAndCacheResponse()
        {
            var factory = new WebApplicationFactory<WebApp2.Program>();

            var client = factory.CreateDefaultClient(new RevalidateDelegate());

            var result1 = await client.GetStringAsync("/");
            var result2 = await client.GetStringAsync("/");

            Assert.That(result1, Is.EqualTo(result2));
        }
    }
}
