using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Context {
    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Url> Url { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Url>().HasKey(x => x.UrlId);
            modelBuilder.Entity<Url>().Property(x => x.RedirectUrl).IsRequired();
            modelBuilder.Entity<Url>().HasIndex(x => x.RedirectUrl).IsClustered(false).IsUnique(false);
            modelBuilder.Entity<Url>().ToTable("Url");
        }
    }
}
