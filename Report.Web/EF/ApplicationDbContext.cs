using Microsoft.EntityFrameworkCore;

namespace Report.Web.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<NiftyWeekly> NiftyWeekly { get; set; }

    }
}
