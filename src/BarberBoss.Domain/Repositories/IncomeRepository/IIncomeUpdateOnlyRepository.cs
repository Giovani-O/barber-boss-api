using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.IncomeRepository;

public interface IIncomeUpdateOnlyRepository
{
    /// <summary>
    /// Get income by id for update
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns>Income</returns>
    Task<Income?> GetById(Guid id);
    
    /// <summary>
    /// Updates an income
    /// </summary>
    /// <param name="income">Income</param>
    void Update(Income income);
}