using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindDb;
using NorthwindDb.Models;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<NorthwindContext>(
                opt => opt.UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True"));
            
            var app = builder.Build();

            app.MapGet("/categories", 
                (NorthwindContext db) => db.Categories.ToList());
            app.MapGet("/categories/{id}", 
                (int id, NorthwindContext db) =>
                {
                    var category = db.Categories.Find(id); 
                    return category != null 
                        ? Results.Ok(category) 
                        : Results.NotFound();
                });
            app.MapGet("/categories/{id}/image", 
                (int id, NorthwindContext db)
                    => Results.Bytes(db.Categories.Find(id)?.Picture ?? [], 
                        contentType: "images/png", $"category_{id}.png"));
            app.MapPut("/categories", 
                async ([FromBody] Category category, NorthwindContext db) =>
                {
                    db.Categories.Update(category);
                    await db.SaveChangesAsync();
                });

            app.Run();
        }
    }
}
