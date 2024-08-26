using BarberBoss.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace BarberBoss.Infrastructure.Security.Cryptography;

public class BCrypt : IPasswordEncrypter
{
    /// <summary>
    /// Method to encrypt password
    /// </summary>
    /// <param name="password">string</param>
    /// <returns>string</returns>
    public string Encrypt(string password)
    {
        var passwordHash = BC.HashPassword(password);
        return passwordHash;
    }

    /// <summary>
    /// Check if password matches passwordHash
    /// </summary>
    /// <param name="password">string</param>
    /// <param name="passwordHash">string</param>
    /// <returns>bool</returns>
    public bool Verify(string password, string passwordHash)
    {
        return BC.Verify(password, passwordHash);
    }
}