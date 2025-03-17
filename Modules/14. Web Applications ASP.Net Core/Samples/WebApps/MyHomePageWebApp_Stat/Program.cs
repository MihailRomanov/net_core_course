using HomePageDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyHomePageWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var galeryPath = Path.Combine(builder.Environment.ContentRootPath, "galery");
            var galeryProvider = new PhysicalFileProvider(galeryPath);
            builder.Services.AddKeyedSingleton<IFileProvider>("galeryProvider", galeryProvider);

            builder.Services.AddDirectoryBrowser();

            builder.Services.AddDbContext<HomePageContext>(
                options => options
                    .UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=HomePageDB;"));


            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapPost("/save-feedback", SaveFeedback).DisableAntiforgery();
            app.MapGet("/galery", ShowGalery);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = galeryProvider,
                RequestPath = "/galery"
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = galeryProvider,
                RequestPath = "/galery2"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = galeryProvider,
                RequestPath = "/galery2"
            });

            app.Run();
        }

        private static IResult ShowGalery(
            [FromKeyedServices("galeryProvider")] IFileProvider galeryProvider)
        {
            var list = galeryProvider.GetDirectoryContents("")
                .Select(f => $"<a href='galery/{f.Name}'><img width='100' src='galery/{f.Name}'/></a>");

            return Results.Text(
                "<html><body>"
                + string.Join("<br>", list) + 
                "</body></hml>", 
                "text/html; charset=utf-8");
        }

        private static async Task<IResult> SaveFeedback(
            [FromForm] string name,
            [FromForm] string email,
            [FromForm] string text,
            [FromServices] HomePageContext context)
        {
            context.FeedbackItems.Add(new FeedbackItem
            {
                PersonName = name,
                Email = email,
                Text = text
            });

            await context.SaveChangesAsync();

            return Results.Redirect("./feedback.html");
        }
    }
}
