using Microsoft.EntityFrameworkCore;

namespace MappingSamples.HierarhySamples
{
    public class SampleDB_TPC : SampleDB
    {
        public SampleDB_TPC(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().UseTpcMappingStrategy();
        }

    }

    public class TPCSample : BaseSample
    {
        [Test]
        public void CreateDB()
        {
            using (var db = new SampleDB_TPC(DbOptions))
            {
                RecreateDB(db);
                HierarchySamplesHelper.WriteAndRead(db);
            }
        }
    }
}
