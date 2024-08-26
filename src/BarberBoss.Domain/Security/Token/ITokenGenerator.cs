using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Security.Token;

public interface ITokenGenerator
{
    string Generate(User user);
}