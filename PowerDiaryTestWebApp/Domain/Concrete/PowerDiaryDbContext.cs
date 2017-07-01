using System.Data.Entity;
using PowerDiaryTestWebApp.Domain.Data;

namespace PowerDiaryTestWebApp.Domain.Concrete
{
    public class PowerDiaryDbContext : DbContext
    {
        public DbSet<CompressedRoute> CompressedRoutes { get; set; }

        public PowerDiaryDbContext() : base("name=EFDbContext")
        {
        }
    }
}