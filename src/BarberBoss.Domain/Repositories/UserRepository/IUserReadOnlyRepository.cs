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
    /// <param name="id">long</param>
    /// <returns>User</returns>
    Task<User?> GetById(long id);
    
    /// <summary>
    /// Checks if a user exists
    /// </summary>
    /// <param name="email">string</param>
    /// <returns>boo</returns>
    Task<bool> CheckIfUserExists(string email);
}