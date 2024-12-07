using Microsoft.EntityFrameworkCore;

namespace MappingSamples.Associations.ManyToOne
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
        public virtual Person Person { get; set; }
        //public int PersonId { get; set; }
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
                .WithOne(e => e.Person)
                .HasForeignKey("FkPersonId")
                //.HasForeignKey(e => e.PersonId)
                .IsRequired(true);
        }
    }


    public class ManyToOneSample : BaseSample
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