using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.BarberShopRepository;

public interface IBarberShopWriteOnlyRepository
{
    /// <summary>
    /// Add a new barber shop
    /// </summary>
    /// <param name="barberShop">BarberShop</param>
    Task Add(BarberShop barberShop);
    
    /// <summary>
    /// Delete a barber shop by id
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>bool</returns>
    Task<bool> Delete(long id);
}