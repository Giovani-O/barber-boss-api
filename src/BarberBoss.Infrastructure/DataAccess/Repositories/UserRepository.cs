using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;

public class UserRepository(BarberBossDbContext dbContext)
    : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly BarberBossDbContext _dbContext = dbContext;

    /// <summary>
    /// Get all users from the database.
    /// </summary>
    /// <returns>List of User</returns>
    public async Task<List<User>> GetAll()
    {
        return await _dbContext.Users.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Get a user by id from the database.
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>User</returns>
    public async Task<User?> GetById(long id)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    /// <summary>
    /// Updates a user in the database.
    /// </summary>
    /// <param name="user">User</param>
    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }
    
    /// <summary>
    /// Get a user by id for update from the database.
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>User</returns>
    public async Task<User?> GetByIdForUpdate(long id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
    
    /// <summary>
    /// Adds a new user to the database.
    /// </summary>
    /// <param name="user">User</param>
    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    /// <summary>
    /// Delete a user by id from the database.
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>bool</returns>
    public async Task<bool> Delete(long id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null)
        {
            return false;
        }
        
        _dbContext.Users.Remove(user);
        return true;
    }
}