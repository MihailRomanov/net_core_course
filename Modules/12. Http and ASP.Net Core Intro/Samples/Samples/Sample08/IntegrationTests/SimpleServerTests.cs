using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SimpleServer;

namespace IntegrationTests
{
    internal class SimpleServerTests
    {
        [Test]
        public async Task Server_should_return_Hello_string()
        {
            WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>();

            var client = factory.CreateClient();
            var resultString = await client.GetStringAsync("/");

            resultString.Should().Be("Hello");
        }
    }
}
