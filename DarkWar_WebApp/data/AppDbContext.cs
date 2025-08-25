using DarkWar_WebApp.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DarkWar_WebApp.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<CPEntry> CPEntries { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=game.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CPEntry-Konfiguration
            modelBuilder.Entity<CPEntry>()
                        .HasOne(c => c.Player)
                        .WithMany(p => p.CP_List)
                        .HasForeignKey(c => c.PlayerID)
                        .OnDelete(DeleteBehavior.Cascade);

            // Event-Konfiguration
            modelBuilder.Entity<Events>()
                        .HasOne(e => e.Player)
                        .WithMany(p => p.Events)
                        .HasForeignKey(e => e.PlayerID)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
