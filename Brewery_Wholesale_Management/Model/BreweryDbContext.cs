using Microsoft.EntityFrameworkCore;

namespace Brewery_Wholesale_Management.Model
{
    public class BreweryDbContext : DbContext
    {
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<WholesalerStock> WholesalerStocks { get; set; }

        public BreweryDbContext(DbContextOptions<BreweryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships and constraints here
            modelBuilder.Entity<Brewery>()
                .HasMany(b => b.Beers)
                .WithOne(b => b.Brewery)
                .HasForeignKey(b => b.BreweryId);

            modelBuilder.Entity<Beer>()
                .HasMany(b => b.WholesalerStocks)
                .WithOne(ws => ws.Beer)
                .HasForeignKey(ws => ws.BeerId);

            modelBuilder.Entity<Wholesaler>()
                .HasMany(w => w.WholesalerStocks)
                .WithOne(ws => ws.Wholesaler)
                .HasForeignKey(ws => ws.WholesalerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
