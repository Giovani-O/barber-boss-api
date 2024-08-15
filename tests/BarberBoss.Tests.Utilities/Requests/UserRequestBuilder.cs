using BarberBoss.Communication.DTOs.Request.UserRequests;
using Bogus;

namespace BarberBoss.Tests.Utilities.Requests;

public class UserRequestBuilder
{
    public static RequestUpdateUserJson Build()
    {   
        return new Faker<RequestUpdateUserJson>()
            .RuleFor(u => u.Id, faker => faker.Random.Long(10000, 99999))
            .RuleFor(u => u.Name, faker => faker.Name.FullName())
            .RuleFor(u => u.Email, faker => faker.Internet.Email())
            .RuleFor(u => u.Password, faker => faker.Internet.Password(10));
    }
}