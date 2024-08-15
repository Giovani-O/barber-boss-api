using BarberBoss.Communication.DTOs.Request.BarberShopRequests;
using Bogus;

namespace BarberBoss.Tests.Utilities.Requests;

public class NewBarberShopRequestBuilder
{
    public static RequestRegisterBarberShopJson Build()
    {
        return new Faker<RequestRegisterBarberShopJson>()
            .RuleFor(x => x.Name, f => f.Company.CompanyName())
            .RuleFor(x => x.UserId, f => f.Random.Long());
    }
}