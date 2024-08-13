using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.BarberShopRepository;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;

internal class BarberShopRepository(BarberBossDbContext dbContext) : IBarberShopReadOnlyRepository,
    IBarberShopWriteOnlyRepository, IBarberShopUpdateOnlyRepository
{
    private readonly BarberBossDbContext _dbContext = dbContext;

    /// <summary>
    /// Get all barber shops of a user from the database.
    /// </summary>
    /// <param name="userId">Guid</param>
    /// <returns>List of BarberShop</returns>
    public async Task<List<BarberShop>> GetAllByUserId(Guid userId)
    {
        return await _dbContext.BarberShops.AsNoTracking().Where(b => b.UserId == userId).ToListAsync();
    }

    /// <summary>
    /// Get a barber shop by id from the database.
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns>BarberShop?</returns>
    async Task<BarberShop?> IBarberShopReadOnlyRepository.GetById(Guid id)
    {
        return await _dbContext.BarberShops.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    }

    /// <summary>
    /// Updates a barber shop in the database.
    /// </summary>
    /// <param name="barberShop">BarberShop</param>
    public void Update(BarberShop barberShop)
    {
        _dbContext.BarberShops.Update(barberShop);
    }
    
    /// <summary>
    /// Get a barber shop by id for update from the database.
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns>BarberShop?</returns>
    async Task<BarberShop?> IBarberShopUpdateOnlyRepository.GetById(Guid id)
    {
        return await _dbContext.BarberShops.FirstOrDefaultAsync(b => b.Id == id);
    }

    /// <summary>
    /// Adds a new barber shop to the database.
    /// </summary>
    /// <param name="barberShop">BarberShop</param>
    public async Task Add(BarberShop barberShop)
    {
        await _dbContext.BarberShops.AddAsync(barberShop);
    }
    
    /// <summary>
    /// Delete a barber shop by id from the database.
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns>bool</returns>
    public async Task<bool> Delete(Guid id)
    {
        var barberShop = await _dbContext.BarberShops.FirstOrDefaultAsync(b => b.Id == id);
        if (barberShop is null)
        {
            return false;
        }
        
        _dbContext.BarberShops.Remove(barberShop);
        return true;
    }
}