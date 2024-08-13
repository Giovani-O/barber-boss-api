using BarberBoss.Domain.Entities;
using Microsoft.EntityFrameworkCore;

/* Migrations, should be run inside of src
 *
 * dotnet ef migrations add InitialMigration --startup-project ./BarberBoss.API/ --project ./BarberBoss.Infrastructure/
 * dotnet ef database update --startup-project ./BarberBoss.API/ --project ./BarberBoss.Infrastructure/
 *
 * Apenas uma dica, caso você tenha problemas com migrações no futuro:
 * Verifique o .csproj e veja se tem alguma tag <Compile /> ;)
 * 
 */

namespace BarberBoss.Infrastructure.DataAccess;
internal class BarberBossDbContext(DbContextOptions<BarberBossDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<BarberShop> BarberShops { get; set; }
    public DbSet<Income> Incomes { get; set; }
}