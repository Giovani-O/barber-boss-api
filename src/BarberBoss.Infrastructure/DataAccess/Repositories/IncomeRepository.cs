using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.IncomeRepository;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;

internal class IncomeRepository(BarberBossDbContext dbContext)
    : IIncomeReadOnlyRepository, IIncomeWriteOnlyRepository, IIncomeUpdateOnlyRepository
{
    private readonly BarberBossDbContext _dbContext = dbContext;

    /// <summary>
    /// Get all incomes for a barber shop from the database.
    /// </summary>
    /// <param name="barberShopId">Guid</param>
    /// <returns>List of Income</returns>
    public async Task<List<Income>> GetAllByBarberShopId(Guid barberShopId)
    {
        return await _dbContext.Incomes.AsNoTracking().Where(i => i.BarberShopId == barberShopId).ToListAsync();
    }

    /// <summary>
    /// Get income by id from the database.
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns>Income?</returns>
    async Task<Income?> IIncomeReadOnlyRepository.GetById(Guid id)
    {
        return await _dbContext.Incomes.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
    }

    /// <summary>
    /// Updates an income in the database.
    /// </summary>
    /// <param name="income">Income</param>
    public void Update(Income income)
    {
        _dbContext.Incomes.Update(income);
    }

    /// <summary>
    /// Get income by id for update from the database.
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns>Income?</returns>
    async Task<Income?> IIncomeUpdateOnlyRepository.GetById(Guid id)
    {
        return await _dbContext.Incomes.FirstOrDefaultAsync(i => i.Id == id);
    }
    
    /// <summary>
    /// Adds an income to the database.
    /// </summary>
    /// <param name="income">Income</param>
    public async Task Add(Income income)
    {
        await _dbContext.Incomes.AddAsync(income);
    }

    /// <summary>
    /// Delete an income by id from the database.
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns>bool</returns>
    public async Task<bool> Delete(Guid id)
    {
        var income = await _dbContext.Incomes.FindAsync(id);
        if (income is null)
        {
            return false;
        }
        
        _dbContext.Incomes.Remove(income);
        return true;
    }
}