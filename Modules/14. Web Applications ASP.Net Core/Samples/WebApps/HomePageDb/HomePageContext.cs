using Microsoft.EntityFrameworkCore;

namespace HomePageDb
{
    public class HomePageContext: DbContext
    {
        public HomePageContext(DbContextOptions<HomePageContext> options) : base(options)
        {
        }

        public DbSet<FeedbackItem> FeedbackItems { get; set; }
    }
}
