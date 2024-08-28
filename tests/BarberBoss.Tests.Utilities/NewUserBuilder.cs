using BarberBoss.Domain.Entities;
using Bogus;

namespace BarberBoss.Tests.Utilities;

public class NewUserBuilder
{
    /// <summary>
    /// Builds a User object with random data and without id.
    /// </summary>
    /// <returns></returns>
    public static User Build()
    {   
        return new Faker<User>()
            .RuleFor(u => u.UserIdentifier, faker => faker.Random.Guid())
            .RuleFor(u => u.Name, faker => faker.Name.FullName())
            .RuleFor(u => u.Email, faker => faker.Internet.Email())
            .RuleFor(u => u.Password, faker => 
                faker.Internet.Password(10, false, @"^[a-zA-Z0-9\!\@\#\$\%\&\*\(\)\.\,\<\>\;\:\/\?\|]+$"));
    }
}