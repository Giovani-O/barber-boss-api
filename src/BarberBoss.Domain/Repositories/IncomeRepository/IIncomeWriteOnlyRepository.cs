using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.IncomeRepository;

public interface IIncomeWriteOnlyRepository
{
    /// <summary>
    /// Adds a new income
    /// </summary>
    /// <param name="income">Income</param>
    Task Add(Income income);

    /// <summary>
    /// Deletes an income by id
    /// </summary>
    /// <param name="id">long</param>
    Task<bool> Delete(long id);
}