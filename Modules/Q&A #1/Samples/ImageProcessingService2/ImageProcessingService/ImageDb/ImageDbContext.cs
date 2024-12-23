using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ImageProcessingService.ImageDb
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions<ImageDbContext> options) : base(options)
        { }

        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(
                b => b.Log(
                    (CoreEventId.ContextDisposed, LogLevel.Information),
                    (CoreEventId.ContextInitialized, LogLevel.Information)));
        }
    }
}
