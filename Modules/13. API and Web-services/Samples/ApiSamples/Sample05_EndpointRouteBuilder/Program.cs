using Microsoft.AspNetCore.Routing.Patterns;

namespace Sample05_EndpointRouteBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(ApiController).Assembly);
            var app = builder.Build();


            app.MapDefaultControllerRoute();

            app.MapGet("/",
                async (context) => await context.Response.WriteAsync("3"));

            ((IEndpointRouteBuilder)app).DataSources.Add(
                new DefaultEndpointDataSource(
                   new RouteEndpoint(
                    async (context) => await context.Response.WriteAsync("ff"),
                    RoutePatternFactory.Parse("/api/f"),
                    0,
                    null,
                    "My endpoint")));

            app.Run();

        }
    }
}
