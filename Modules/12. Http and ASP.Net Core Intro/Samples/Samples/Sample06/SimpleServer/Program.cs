using Humanizer;
using Microsoft.Extensions.FileProviders;
using System;

namespace SimpleServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            var fileProvider = new EmbeddedFileProvider(typeof(Program).Assembly);

            var httpSamples = app.MapGroup("/http_samples");
            httpSamples.MapGet("/form", context =>
                context.Response.SendFileAsync(fileProvider.GetFileInfo("/Forms.form.html")));
            httpSamples.MapPost("/form", context => context.ProcessLogin());

            var sayRoute = app.MapGroup("/say/");

            sayRoute.MapGet("/hello", (string? name) => GetHelloByeString("Hello", name));
            sayRoute.MapGet("/bye", (string? name) => GetHelloByeString("Bye", name));
            sayRoute.MapGet("/", () => "What???");

            app.MapGet("/say_ext/{word}",
                (string word, string? name) => GetHelloByeString(word, name));
            app.MapGet("/say_ext2/{word:regex(^hello|bye$)}",
                (string word, string? name) => GetHelloByeString(word, name));

            app.MapFallback(context => context.Response.WriteAsync("Something went wrong..."));

            app.Run();
        }

        private static string GetHelloByeString(string helloBye, string? name)
        {
            name = string.IsNullOrEmpty(name) ? "World" : name;
            return $"{helloBye.Transform(To.TitleCase)}, {name}\n";
        }

    }
}
