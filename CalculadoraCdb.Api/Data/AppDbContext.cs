using CalculadoraCdb.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraCdb.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CalculoCdb> CalculosCdb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalculoCdb>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ValorInvestido).HasPrecision(18, 4);
                entity.Property(e => e.ValorBruto).HasPrecision(18, 4);
                entity.Property(e => e.ValorLiquido).HasPrecision(18, 4);
            });
        }
    }
}
