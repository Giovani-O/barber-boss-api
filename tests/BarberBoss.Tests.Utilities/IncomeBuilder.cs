using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Enums;
using Bogus;

namespace BarberBoss.Tests.Utilities.Requests;

public class IncomeBuilder
{
    /// <summary>
    /// Builds a new Income object with random data.
    /// </summary>
    /// <returns>Income</returns>
    public static Income Build()
    {
        return new Faker<Income>()
            .RuleFor(i => i.Id, faker => faker.Random.Long(10000, 99999))
            .RuleFor(i => i.Title, faker => faker.Commerce.ProductName())
            .RuleFor(i => i.Description, faker => faker.Lorem.Sentence())
            .RuleFor(i => i.ServiceDate, faker => faker.Date.Soon())
            .RuleFor(i => i.PaymentType, faker => faker.PickRandom<PaymentType>().ToString())
            .RuleFor(i => i.Price, faker => faker.Random.Decimal(1, 1000))
            .RuleFor(i => i.BarberShopId, faker => faker.UniqueIndex);
    }
}