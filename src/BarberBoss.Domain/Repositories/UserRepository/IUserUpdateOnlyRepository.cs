using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.UserRepository;

public interface IUserUpdateOnlyRepository
{
    /// <summary>
    /// Get user by id for update
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>User</returns>
    Task<User?> GetByIdForUpdate(long id);
    
    /// <summary>
    /// Updates a user
    /// </summary>
    /// <param name="user">User</param>
    void Update(User  user);
}