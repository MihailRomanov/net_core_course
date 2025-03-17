using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders.Physical;

namespace WebApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMemoryCache();
            //builder.Services.AddDistributedMemoryCache();
            
            builder.Services.AddDistributedSqlServerCache(
                options =>
                {
                    options.ConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Cache;";
                    options.SchemaName = "dbo";
                    options.TableName = "Cache";
                });
            var app = builder.Build();

            app.MapGet("/memory/{key}", ([FromServices] IMemoryCache cache, string key) =>
            {
                var value = cache.GetOrCreate(key, ce =>
                {
                    return Random.Shared.Next(1, 100);
                });
                return Results.Text($"{value}");
            });

            app.MapGet("/memory/file",
                (
                    [FromServices] IMemoryCache cache,
                    [FromServices] IHostEnvironment environment
                ) =>
                {
                    var value = cache.GetOrCreate("file", ce =>
                    {
                        Console.WriteLine(ce.Value);
                        ce.SetSlidingExpiration(TimeSpan.FromSeconds(20));

                        var fileName = Path.Combine(
                            environment.ContentRootPath, "dir", "file.txt");

                        var file = new FileInfo(fileName);

                        if (file.Exists)
                        {
                            ce.AddExpirationToken(new PollingFileChangeToken(file));
                        }

                        return file.Exists
                            ? File.ReadAllText(fileName)
                            : "";
                    });

                    return Results.Text($"{value}");
                });

            app.MapGet("/dist/{key}", ([FromServices] IDistributedCache cache, string key) =>
            {
                var value = cache.GetString(key);
                if (value == null)
                {
                    value = Random.Shared.Next(1, 100).ToString();
                    cache.SetString(key, value);
                }
                return Results.Text($"{value}");
            });

            app.Run();
        }
    }
}
