using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.UserRepository;

public interface IUserUpdateOnlyRepository
{
    /// <summary>
    /// Get user by id for update
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns>User</returns>
    Task<User?> GetById(Guid id);
    
    /// <summary>
    /// Updates a user
    /// </summary>
    /// <param name="user">User</param>
    Task Update(User  user);
}