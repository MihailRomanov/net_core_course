using AuthLib;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SimpleServer;
using System.Net;

namespace IntegrationTests
{
    public class AuthLibTests
    {
        [Test]
        [TestCase("123", HttpStatusCode.OK)]
        [TestCase("456", HttpStatusCode.Unauthorized)]
        public async Task Server_should_return_AutorizationResult(
            string pass, HttpStatusCode statusCode)
        {
            var mockStore = new Mock<IFailCountStore>();
            mockStore.Setup(s => s.Fails).Returns(0);

            WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => 
                {
                    builder.Configure(app =>
                    {
                        app.UsePasswordAuth("123");
                        app.Run(context => context.Response.WriteAsync("Hello"));
                    });
                    builder.ConfigureServices(serviceCollection =>
                        serviceCollection.AddSingleton(mockStore.Object));
                });

            var client = factory.CreateClient();
            var result = await client.GetAsync($"/?pass={pass}");

            result.StatusCode.Should().Be(statusCode);
        }
    }
}
