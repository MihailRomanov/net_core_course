using HomePageDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

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

            builder.Services.AddMvc();

            var app = builder.Build();


            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            app.UseStaticFiles();
            
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
    }
}
