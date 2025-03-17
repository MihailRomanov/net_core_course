using Microsoft.AspNetCore.Mvc;
using RouterResearchApplication;

namespace Sample06_RouteTemplates
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddTransient<ControllerBase, ApiController>();
            var app = builder.Build();

            app.UseRouter(new CustomControllerRouter());

            app.Run();
        }
    }
}
