using BarberBoss.Domain.Repositories;

namespace BarberBoss.Infrastructure.DataAccess;

internal class UnitOfWork(BarberBossDbContext context) : IUnitOfWork
{
    /// <summary>
    /// Save the changes made to the database
    /// </summary>
    public async Task Commit()
    {
        await context.SaveChangesAsync();
    }
}
