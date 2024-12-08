using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TestingAndDebuggingSamples.Model;

namespace TestingAndDebuggingSamples
{
    public class LoggingSamples
    {
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";

        [Test]
        public void SimpleLogging()
        {
            var options = new DbContextOptionsBuilder<NorthwindContext>()
                .UseSqlServer(ConnectionString)
                .LogTo(message => Console.WriteLine(message),
                    new[] { RelationalEventId.CommandExecuting } )
                .Options;

            using (var db = new NorthwindContext(options))
            {
                var products = db.Products
                    .Where(p => p.Discontinued)
                    .Include(p => p.Category)
                    .ToList();
            }

        }
    }
}
