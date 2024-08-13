using BarberBoss.Domain.Entities;
using Microsoft.EntityFrameworkCore;

/* Migrations, should be run inside of src
 *
 * dotnet ef migrations add InitialMigration --startup-project ./BarberBoss.API/ --project ./BarberBoss.Infrastructure/
 * dotnet ef database update --startup-project ./BarberBoss.API/ --project ./BarberBoss.Infrastructure/
 * 
 */

namespace BarberBoss.Infrastructure.DataAccess;
internal class BarberBossDbContext(DbContextOptions<BarberBossDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<BarberShop> BarberShops { get; set; }
    public DbSet<Income> Incomes { get; set; }
}