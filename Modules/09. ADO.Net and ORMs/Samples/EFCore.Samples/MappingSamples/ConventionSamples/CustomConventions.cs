using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Text.RegularExpressions;

namespace MappingSamples.ConventionSamples.Custom
{
    public class Category
    {
        public int CategoryCode { get; set; }
        public string Name { get; set; }
    }

    public class Product
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }

    public class SampleDB : DbContext
    {
        public SampleDB(DbContextOptions options) 
            : base(options) { }
        public DbSet<Product> Products { get; set; }
        protected override void ConfigureConventions(
            ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new CodeToId());
        }
    }

    public class CodeToId : IEntityTypeAddedConvention
    {
        public void ProcessEntityTypeAdded(
            IConventionEntityTypeBuilder entityTypeBuilder, 
            IConventionContext<IConventionEntityTypeBuilder> context)
        {
            var metadata = entityTypeBuilder.Metadata;
            var codeProperty = metadata
                .GetDeclaredProperties()
                .SingleOrDefault(p => p.ClrType == typeof(int)
                    && p.Name.EndsWith("Code"));

            if (codeProperty != null &&
                metadata.FindPrimaryKey() == null)
            {
                var columnName = 
                    Regex.Replace(codeProperty.Name, "Code$", "Id");
                codeProperty.SetColumnName(columnName);

                metadata.SetPrimaryKey(codeProperty);
            }
        }
    }


    public class CustomConventions: BaseSample
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
