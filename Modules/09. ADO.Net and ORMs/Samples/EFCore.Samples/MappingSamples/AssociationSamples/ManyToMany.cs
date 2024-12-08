using Microsoft.EntityFrameworkCore;

namespace MappingSamples.Associations.ManyToMany
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<Address> DeliveryAddresses { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string AddressString { get; set; }
        public virtual IList<Person> People { get; set; }
        
    }

    public class SampleDB : DbContext
    {
        public SampleDB(DbContextOptions options) : base(options) { }
        public DbSet<Person> People { get; set; }
    }

    public class SampleDB_Custom : DbContext
    {
        public SampleDB_Custom(DbContextOptions options) : base(options) { }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(e => e.DeliveryAddresses)
                .WithMany(e => e.People)
                .UsingEntity(
                    "AddressPeopleReference",
                    je => je.HasOne(typeof(Address)).WithMany()
                        .HasForeignKey("aId"),
                    je => je.HasOne(typeof(Person)).WithMany()
                        .HasForeignKey("pId")
                    );
        }
    }


    public class ManyToManySample : BaseSample
    {
        [Test]
        public void Default()
        {
            using (var db = new SampleDB(DbOptions))
            {
                RecreateDB(db);
            }
        }

        [Test]
        public void Custom()
        {
            using (var db = new SampleDB_Custom(DbOptions))
            {
                RecreateDB(db);
            }
        }
    }
}