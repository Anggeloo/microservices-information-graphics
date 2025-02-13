using Microsoft.EntityFrameworkCore;

namespace microservices_information_graphics.Database
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Models.InformationGraphic> InformationGraphic { get; set; }
    }
}
