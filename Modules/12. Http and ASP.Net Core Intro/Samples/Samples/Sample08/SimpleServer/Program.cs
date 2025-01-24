namespace SimpleServer
{
    public class Program
    {
        internal static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();

            app.Run(context => context.Response.WriteAsync("Hello"));

            app.Run();
        }
    }
}