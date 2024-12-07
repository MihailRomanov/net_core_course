using Microsoft.EntityFrameworkCore;

namespace MappingSamples
{
    public class BaseSample
    {
        const string ConnectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True";

        protected DbContextOptions DbOptions;

        [SetUp]
        public void SetUp()
        {
            DbOptions = new DbContextOptionsBuilder()
                .UseSqlServer(ConnectionString)
                .Options;
        }

        protected void RecreateDB(DbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            if (dbContext.Database.IsSqlServer())
                dbContext.Database.ExecuteSql($"exec sp_changedbowner 'sa'");
        }
    }
}