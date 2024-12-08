using Microsoft.EntityFrameworkCore;

namespace MappingSamples.Associations.OneToOne
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Address DeliveryAddress { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string AddressString { get; set; }
        public virtual Person Person { get; set; }
        //public int PersonId { get; set; }
    }

    public class SampleDB : DbContext
    {
        public SampleDB(DbContextOptions options) : base(options) { }
        public DbSet<Person> People { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(e => e.DeliveryAddress)
                .WithOne(e => e.Person)
                .HasForeignKey<Address>("PersonId")
                .IsRequired(true);
        }
    }

    public class OneToOneSample : BaseSample
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