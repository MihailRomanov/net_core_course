using Microsoft.EntityFrameworkCore;

namespace EFCoreSamples.Model
{
    public class Northwind : DbContext
    {
        public Northwind(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(
                category =>
                {
                    category
                        .ToTable("Categories", "Northwind")
                        .HasKey(c => c.Id);
                    category.Property(c => c.Id)
                        .HasColumnName("CategoryID")
                        .ValueGeneratedOnAdd();
                    category.Property(c => c.Name)
                        .HasColumnName("CategoryName").IsRequired().HasMaxLength(15);
                    category.Property(c => c.Description).HasColumnType("ntext");
                    category.Property(c => c.Picture).HasColumnType("image");
                });

            modelBuilder.Entity<Product>(
                product =>
                {
                    product
                        .ToTable("Products", "Northwind")
                        .HasKey(p => p.Id);
                    product.Property(p => p.Id)
                        .HasColumnName("ProductID")
                        .ValueGeneratedOnAdd();
                    product.Property(p => p.Name)
                        .HasColumnName("ProductName")
                        .IsRequired()
                        .HasMaxLength(40);
                    product.Property(p => p.QuantityPerUnit).HasMaxLength(20);
                    product.Property(p => p.UnitPrice)
                        .HasColumnType("money")
                        .HasPrecision(19, 4);
                    product.HasOne(p => p.Category)
                        .WithMany(c => c.Products)
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Products_Categories");
                });
        }
    }
}
