using Microsoft.EntityFrameworkCore;

namespace MappingSamples.HierarhySamples
{
    public class SampleDB_TPT1 : SampleDB
    {
        public SampleDB_TPT1(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<ContactPerson>().ToTable("ContactPersons");
        }
    }

    public class SampleDB_TPT2 : SampleDB
    {
        public SampleDB_TPT2(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().UseTptMappingStrategy();
        }
    }


    public class TPTSample : BaseSample
    {
        [Test]
        public void DirectTableMap()
        {
            using (var db = new SampleDB_TPT1(DbOptions))
            {
                RecreateDB(db);
                HierarchySamplesHelper.WriteAndRead(db);
            }
        }

        [Test]
        public void UseStrategy()
        {
            using (var db = new SampleDB_TPT2(DbOptions))
            {
                RecreateDB(db);
                HierarchySamplesHelper.WriteAndRead(db);
            }
        }

    }
}
