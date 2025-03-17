using Microsoft.AspNetCore.Mvc;

namespace Sample07_RequestDelegateFactory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddTransient<ControllerBase, Api2Controller>();
            builder.Services.AddTransient<Api2Controller>();
            var app = builder.Build();

            app.UseRouter(new CustomControllerRouter2());

            app.Run();
        }
    }
}
