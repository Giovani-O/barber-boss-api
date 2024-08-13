using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.UserRepository;

public interface IUserWriteOnlyRepository
{
    /// <summary>
    /// Adds a new user
    /// </summary>
    /// <param name="user">User</param>
    Task Add(User user);
    
    /// <summary>
    /// Delete a user by id
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>bool</returns>
    Task<bool> Delete(long id);
}