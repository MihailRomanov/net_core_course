using AuthLib;
using SimpleServer;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddSingleton<IFailCountStore, InMemoryFailCountStore>();
        var app = builder.Build();

        app.UsePasswordAuth("4567");

        app.Use(async (context, next) =>
        {
            await next.Invoke();

            var name = context.Request.Query["name"];
            name = string.IsNullOrEmpty(name) ? "World" : name;
            
            var failCount = context.RequestServices.GetService<IFailCountStore>()!.Fails;

            await context.Response.WriteAsync($"Hello, {name}\n");
            await context.Response.WriteAsync($"Fail count = {failCount}\n");
        });

        app.MapGet("/", (HttpContext context, IFailCountStore store) 
            => context.Response.WriteAsync($"Simple server ({store.Fails})!\n"));

        app.Run();
    }
}