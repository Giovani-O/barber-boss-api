using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.BarberShopRepository;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;

public class BarberShopRepository(BarberBossDbContext dbContext) : IBarberShopReadOnlyRepository,
    IBarberShopWriteOnlyRepository, IBarberShopUpdateOnlyRepository
{
    private readonly BarberBossDbContext _dbContext = dbContext;

    /// <summary>
    /// Get all barber shops of a user from the database.
    /// </summary>
    /// <param name="userId">long</param>
    /// <returns>List of BarberShop</returns>
    public async Task<List<BarberShop>> GetAllByUserId(long userId)
    {
        return await _dbContext.BarberShops.AsNoTracking().Where(b => b.UserId == userId).ToListAsync();
    }

    /// <summary>
    /// Get a barber shop by id from the database.
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>BarberShop?</returns>
    public async Task<BarberShop?> GetById(long id)
    {
        return await _dbContext.BarberShops.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<bool> CheckIfBarberShopExists(string name)
    {
        var result = await _dbContext.BarberShops.AsNoTracking().FirstOrDefaultAsync(b => b.Name == name);

        return result is not null;
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
    /// <param name="id">long</param>
    /// <returns>BarberShop?</returns>
    public async Task<BarberShop?> GetByIdForUpdate(long id)
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
    /// <param name="id">long</param>
    /// <returns>bool</returns>
    public async Task<bool> Delete(long id)
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