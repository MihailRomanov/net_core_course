using Microsoft.EntityFrameworkCore;

namespace MappingSamples.ComplexTypes
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address DeliveryAddress { get; set; }
        public Address BillingAddress { get; set; }
        public int[]? Numbers { get; set; }
    }

    public class Person2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Address> Addresses { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }


    public class SampleDB_OwnedTypes : DbContext
    {
        public SampleDB_OwnedTypes(DbContextOptions options) : base(options) { }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(person =>
            {
                person.OwnsOne(e => e.DeliveryAddress);
                person.OwnsOne(e => e.BillingAddress);
            });
        }
    }

    public class SampleDB_OwnedTypesCollections : DbContext
    {
        public SampleDB_OwnedTypesCollections(DbContextOptions options) : base(options) { }
        public DbSet<Person2> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person2>(person =>
            {
                person.OwnsMany(e => e.Addresses);
            });
        }
    }

    public class SampleDB_ComplexProperties : DbContext
    {
        public SampleDB_ComplexProperties(DbContextOptions options) : base(options) { }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(person =>
            {
                person.ComplexProperty(e => e.DeliveryAddress);
                person.ComplexProperty(e => e.BillingAddress);
            });
        }
    }


    public class ComplexTypesSample : BaseSample
    {
        [Test]
        public void OwnedTypes()
        {
            using (var db = new SampleDB_OwnedTypes(DbOptions))
            {
                RecreateDB(db);
            }
        }

        [Test]
        public void OwnedTypesCollections()
        {
            using (var db = new SampleDB_OwnedTypesCollections(DbOptions))
            {
                RecreateDB(db);
            }
        }

        [Test]
        public void ComplexProperties()
        {
            using (var db = new SampleDB_ComplexProperties(DbOptions))
            {
                RecreateDB(db);

                var address = new Address
                {
                    City = "c",
                    Street = "s",
                    ZipCode = "z"
                };
                db.People.Add(new Person
                {
                    Name = "Name1",
                    Numbers = [2, 5, 7, 12],
                    BillingAddress = address,
                    DeliveryAddress = address,
                });
                db.SaveChanges();
            }
        }
    }
}