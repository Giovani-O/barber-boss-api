using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.IncomeRepository;

public interface IIncomeReadOnlyRepository
{
    /// <summary>
    /// Get all incomes for a user
    /// </summary>
    /// <param name="barberShopId">long</param>
    /// <returns>List of Income</returns>
    Task<List<Income>> GetAllByBarberShopId(long barberShopId);
    
    /// <summary>
    /// Get income by id
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>Income | null</returns>
    Task<Income?> GetById(long id);
}