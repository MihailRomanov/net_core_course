namespace Sample03_MVCRouter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddMvcCore(
                setup => setup.EnableEndpointRouting = false)
                .AddApplicationPart(typeof(ApiController).Assembly);
            var app = builder.Build();

            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "/{controller}/{action}",
                    new { controller = "api", action = "three" });
                routes.MapGet("/api/f",
                    context => context.Response.WriteAsync("ff"));
            });

            app.Run();

        }
    }
}
