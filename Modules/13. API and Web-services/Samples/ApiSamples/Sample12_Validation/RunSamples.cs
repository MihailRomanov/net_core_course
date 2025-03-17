using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using Sample12_Validation.Custom;
using System.Net;

namespace Sample12_Validation
{
    public class RunSamples
    {
        [TestCase("/api/check")]
        [TestCase("/api2/check")]
        public async Task Sample01(string url)
        {
            var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();

            var result = await client.PostAsJsonAsync(url, new Person());
            var problemDetails = await result.Content
                .ReadFromJsonAsync<ValidationProblemDetails>();
            
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            problemDetails.Errors.ToList()
                .ForEach(e => 
                    Console.WriteLine($"{e.Key} - {string.Join("\n", e.Value)}"));
        }

    }
}
