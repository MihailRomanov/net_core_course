using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Sample08_CustomBinding
{
    public class RunSamples
    {
        [TestCase("/map/phones/7(3412)43-56-89", "73412435689")]
        [TestCase("/map/products?take=200&skip=300", "PagingParams { Take = 200, Skip = 300 }")]
        [TestCase("/api/phones/7(3412)43-56-89", "73412435689")]
        [TestCase("/api/products?take=200&skip=300", "PagingParams { Take = 200, Skip = 300 }")]
        public async Task Sample01(string url, string expectedResult)
        {
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();

            var result = await client.GetStringAsync(url);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

    }
}
