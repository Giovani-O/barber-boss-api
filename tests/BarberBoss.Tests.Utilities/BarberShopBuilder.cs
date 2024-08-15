using BarberBoss.Domain.Entities;
using Bogus;

namespace BarberBoss.Tests.Utilities.Requests;

public class BarberShopBuilder
{
    /// <summary>
    /// Builds a BarberShop object with random data.
    /// </summary>
    /// <returns></returns>
    public static BarberShop Build()
    {
        return new Faker<BarberShop>()
            .RuleFor(b => b.Id, faker => faker.Random.Long(10000, 99999))
            .RuleFor(b => b.Name, faker => faker.Company.CompanyName())
            .RuleFor(b => b.UserId, faker => faker.Random.Long(10000, 99999));
    }
}