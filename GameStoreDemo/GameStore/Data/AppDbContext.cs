using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        
        }

        public DbSet<MCategory> tbl_category { get; set; }

        public DbSet<MConsole> tbl_console { get; set; }

        public DbSet<MProduct> tbl_product { get; set; }
    }
}
