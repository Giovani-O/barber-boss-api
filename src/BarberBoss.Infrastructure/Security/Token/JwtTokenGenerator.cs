using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Security.Token;
using Microsoft.IdentityModel.Tokens;

namespace BarberBoss.Infrastructure.Security.Token;

public class JwtTokenGenerator : ITokenGenerator
{
    private readonly uint _expirationInMinutes;
    private readonly string _signingKey;

    public JwtTokenGenerator(uint expirationInMinutes, string signingKey)
    {   
        _expirationInMinutes = expirationInMinutes;
        _signingKey = signingKey;
    }
    
    /// <summary>
    /// Generate a new JWT token
    /// </summary>
    /// <param name="user">(User) Used for the claims</param>
    /// <returns>(string) JWT Token</returns>
    public string Generate(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationInMinutes),
            SigningCredentials = new SigningCredentials(
                GetSecurityKey(),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Subject = new ClaimsIdentity(claims)
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    /// <summary>
    /// Generate a symmetric security key
    /// </summary>
    /// <returns>(SymmetricSecurityKey) Symmetric key generated</returns>
    private SymmetricSecurityKey GetSecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_signingKey);
        return new SymmetricSecurityKey(key);
    }
}