using Microsoft.EntityFrameworkCore;

namespace MappingSamples.HierarhySamples
{
    public class SampleDB_TPH1 : SampleDB
    {
        public SampleDB_TPH1(DbContextOptions options) : base(options) { }
    }

    public class SampleDB_TPH2 : SampleDB
    {
        public SampleDB_TPH2(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var personEntity = modelBuilder.Entity<Person>();
            personEntity
                .ToTable("AllEntities")
                .HasDiscriminator<string>("Type");
        }
    }

    public class SampleDB_TPH3 : SampleDB
    {
        public SampleDB_TPH3(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var personEntity = modelBuilder.Entity<Person>();
            personEntity
                .ToTable("AllEntities")
                .HasDiscriminator<EntityType>("Type")
                .HasValue<Person>(EntityType.Person)
                .HasValue<ContactPerson>(EntityType.ContactPerson)
                .HasValue<Employee>(EntityType.Employee);
        }

        private enum EntityType
        {
            Person,
            Employee,
            ContactPerson
        }
    }

    public class TPHSample : BaseSample
    {
        [Test]
        public void DefaultConfiguration()
        {
            using (var db = new SampleDB_TPH1(DbOptions))
            {
                RecreateDB(db);
                HierarchySamplesHelper.WriteAndRead(db);
            }
        }

        [Test]
        public void SetTableAndDiscriminatorName()
        {
            using (var db = new SampleDB_TPH2(DbOptions))
            {
                RecreateDB(db);
                HierarchySamplesHelper.WriteAndRead(db);
            }
        }

        [Test]
        public void ChangeDiscriminatorType()
        {
            using (var db = new SampleDB_TPH3(DbOptions))
            {
                RecreateDB(db);
                HierarchySamplesHelper.WriteAndRead(db);
            }
        }
    }
}
