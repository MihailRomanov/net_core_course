using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Sample09_CustomResult
{
    public class RunSamples
    {
        [TestCase("/", "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<string>Hello World!</string>")]
        public async Task Sample01(string url, string expectedResult)
        {
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();

            var result = await client.GetStringAsync(url);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

    }
}
