namespace Sample04_CustomEndpoint
{
    public class Program
    {
        static Dictionary<string, RequestDelegate> handlers =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["/api/one"] =
                async (context) => await context.Response.WriteAsync("1"),
                ["/api/two"] =
                async (context) => await context.Response.WriteAsync("2"),
                ["/api/three"] =
                async (context) => await context.Response.WriteAsync("3"),
                ["/"] =
                async (context) => await context.Response.WriteAsync("3"),
            };


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                var headers = context.Request.Headers;
                if (headers.TryGetValue("XXX", out var key) &&
                    handlers.TryGetValue(key!, out var handler))
                {
                    context.SetEndpoint(new Endpoint(handler, null, null));
                }
                await next.Invoke();
            });

            app.UseEndpoints(_ => { });

            app.Run();

        }
    }
}
