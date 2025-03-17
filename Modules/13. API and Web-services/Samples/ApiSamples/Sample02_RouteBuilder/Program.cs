namespace Sample02_RouteBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();

            app.UseRouter(builder =>
            {
                builder.Routes.Add(
                    new Route(new RouteHandler(
                        context => context.Response.WriteAsync("1")),
                        "/api/one",
                        builder.ServiceProvider
                            .GetRequiredService<IInlineConstraintResolver>())
                    );

                builder.MapGet("/api/two",
                    context => context.Response.WriteAsync("2"));

                builder.DefaultHandler =
                    new RouteHandler(context => context.Response.WriteAsync("3"));

                builder.MapRoute("root", "/");
            });


            app.Run();
        }
    }
}
