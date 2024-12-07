using Microsoft.EntityFrameworkCore;

namespace MappingSamples.ConventionSamples
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }

    public class SampleDB : DbContext
    {
        public SampleDB(DbContextOptions options) 
            : base(options) { }
        public DbSet<Product> Products { get; set; }
    }

    public class BuiltInConventions: BaseSample
    {
        [Test]
        public void CreateDB()
        {
            using (var db = new SampleDB(DbOptions))
            {
                RecreateDB(db);
            }
        }

    }
}
