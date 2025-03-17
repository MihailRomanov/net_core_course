namespace Sample01_CustomIRouter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();

            app.UseRouter(new CustomRouter());

            app.Run();
        }
    }
}
