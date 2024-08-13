using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.UserRepository;

public interface IUserReadOnlyRepository
{
    /// <summary>
    /// Gets all users
    /// </summary>
    /// <returns>List of User</returns>
    Task<List<User>> GetAll();
    
    /// <summary>
    /// Get a user by id
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns>User</returns>
    Task<User> GetById(Guid id);
}