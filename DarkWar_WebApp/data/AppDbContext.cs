using DarkWar_WebApp.Pages;
using Microsoft.EntityFrameworkCore;

namespace DarkWar_WebApp.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
    }
}
