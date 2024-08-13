using BarberBoss.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess;

internal class BarberBossDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<BarberShop> BarberShops { get; set; }
    public DbSet<Income> Incomes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BarberShop>()
            .HasOne(b => b.User)
            .WithMany(u => u.BarberShops)
            .HasForeignKey(b => b.UserId);
        
        modelBuilder.Entity<Income>()
            .HasOne(i => i.BarberShop)
            .WithMany(b => b.Incomes)
            .HasForeignKey(i => i.BarberShopId);
    }
}