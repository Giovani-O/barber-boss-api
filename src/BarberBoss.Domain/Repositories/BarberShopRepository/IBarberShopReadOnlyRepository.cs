using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.BarberShopRepository;

public interface IBarberShopReadOnlyRepository
{
    /// <summary>
    /// Get all barber shops belonging to a user
    /// </summary>
    /// <param name="userId">long</param>
    /// <returns>List of BarberShop</returns>
    Task<List<BarberShop>> GetAllByUserId(long userId);
    
    /// <summary>
    /// Get barber shop by id
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>BarberShop</returns>
    Task<BarberShop?> GetById(long id);
}