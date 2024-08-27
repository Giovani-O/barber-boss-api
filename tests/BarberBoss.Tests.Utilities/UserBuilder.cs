using BarberBoss.Domain.Entities;
using Bogus;

namespace BarberBoss.Tests.Utilities.Requests;

public class UserBuilder
{
    /// <summary>
    /// Builds a User object with random data.
    /// </summary>
    /// <returns></returns>
    public static User Build()
    {   
        return new Faker<User>()
            .RuleFor(u => u.Id, faker => faker.Random.Long(10000, 99999))
            .RuleFor(u => u.UserIdentifier, faker => faker.Random.Guid())
            .RuleFor(u => u.Name, faker => faker.Name.FullName())
            .RuleFor(u => u.Email, faker => faker.Internet.Email())
            .RuleFor(u => u.Password, faker => 
                faker.Internet.Password(10, false, @"^[a-zA-Z0-9\!\@\#\$\%\&\*\(\)\.\,\<\>\;\:\/\?\|]+$"));
    }
}