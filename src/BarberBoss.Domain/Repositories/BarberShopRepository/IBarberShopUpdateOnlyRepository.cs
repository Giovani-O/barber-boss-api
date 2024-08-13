using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.BarberShopRepository;

public interface IBarberShopUpdateOnlyRepository
{
    /// <summary>
    /// Get barber shop by id for update
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>BarberShop</returns>
    Task<BarberShop?> GetById(long id);

    /// <summary>
    /// Updates a barber shop
    /// </summary>
    /// <param name="barberShop">BarberShop</param>
    void Update(BarberShop barberShop);
}