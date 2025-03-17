using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace WebApp2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddResponseCaching();
            builder.Services.AddControllers(options =>
            {
                options.CacheProfiles.Add("Paging50",
                    new CacheProfile
                    {
                        Duration = 50,
                        VaryByQueryKeys = ["take", "skip"],
                        Location = ResponseCacheLocation.Any
                    });
            });
            var app = builder.Build();

            app.UseResponseCaching();

            var counter = 0;

            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().LastModified
                    = DateTimeOffset.UtcNow;
                await next(context);
            });

            app.MapGet("/", (HttpResponse response) =>
                {
                    var headers = response.GetTypedHeaders();

                    headers.CacheControl =
                        new CacheControlHeaderValue
                        {
                            MaxAge = TimeSpan.FromSeconds(5),
                            Public = true,
                        };

                    return counter++.ToString();
                });

            app.MapDefaultControllerRoute();
            app.Run();
        }
    }
}
