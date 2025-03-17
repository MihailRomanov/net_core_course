using Microsoft.AspNetCore.Mvc;

namespace WebApp3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddOutputCache(options =>
            {
                options.AddBasePolicy(
                    p => p.Expire(TimeSpan.FromSeconds(1)));
                options.AddPolicy("Paging50",
                    p => p
                        .Expire(TimeSpan.FromSeconds(5))
                        .SetVaryByQuery("take")
                        .SetVaryByQuery("skip")
                    );
            });
            var app = builder.Build();

            app.UseOutputCache();

            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().LastModified
                    = DateTimeOffset.UtcNow;
                await next(context);
            });

            var counter = 0;
            app.MapGet("/", () => counter++.ToString())
                .CacheOutput(p => p.Expire(TimeSpan.FromSeconds(5)));

            app.MapGet("/add", (
                [FromQuery] int a, [FromQuery] int b)
                    => (a + b).ToString()
                        + "\n"
                        + DateTime.Now.ToLongTimeString())
                .CacheOutput(p => p
                    .Expire(TimeSpan.FromSeconds(50))
                    .SetVaryByQuery("a").SetVaryByQuery("b"));

            app.MapDefaultControllerRoute();
            app.Run();
        }
    }
}
