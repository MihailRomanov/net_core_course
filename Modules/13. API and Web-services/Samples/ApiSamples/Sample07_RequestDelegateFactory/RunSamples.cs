using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Sample07_RequestDelegateFactory
{
    public class RunSamples
    {
        [TestCase("/api2/One", "1")]
        [TestCase("/api2/Two", "2")]
        [TestCase("/", "3")]
        public async Task CustomRouter2(string url, string expectedResult)
        {
            var factory = new WebApplicationFactory<Program>();

            var httpClient = factory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "/");
            request.Headers.Add("XXX", url);
            request.Headers.Add("p", 45.ToString());
            var result = await (await httpClient.SendAsync(request))
                .Content.ReadAsStringAsync();

            Assert.That(result, Is.EqualTo(expectedResult + "45"));
        }

    }
}
