using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NorthwindDb;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<NorthwindContext>(
                opt => opt.UseSqlServer(
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True"));
            
            builder.Services.AddControllers();

            var app = builder.Build();
            app.MapControllers();

            app.Run();
        }
    }
}
