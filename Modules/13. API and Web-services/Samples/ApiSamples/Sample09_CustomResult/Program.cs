namespace Sample09_CustomResult
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => XmlResults.Ok("Hello World!"));

            app.Run();
        }
    }
}
