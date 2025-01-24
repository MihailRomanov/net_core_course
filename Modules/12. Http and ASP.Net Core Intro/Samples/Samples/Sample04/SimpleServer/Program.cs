namespace SimpleServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.Use(async (context, next) =>
            {
                var pass = context.Request.Query["pass"].ToString();
                if (pass.StartsWith("123"))
                {
                    await next.Invoke();
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                }
            });


            app.Use(async (context, next) =>
            {
                await next.Invoke();

                var name = context.Request.Query["name"];
                name = string.IsNullOrEmpty(name) ? "World" : name;

                await context.Response.WriteAsync($"Hello, {name}\n");
            });


            app.Run(context => context.Response.WriteAsync("Simple server!\n"));

            app.Run();
        }
    }
}
