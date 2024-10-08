using BarberBoss.Communication.DTOs.Request.UserRequests;
using Bogus;

namespace BarberBoss.Tests.Utilities.Requests;

public class NewUserRequestBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(u => u.Name, faker => faker.Name.FullName())
            .RuleFor(u => u.Email, faker => faker.Internet.Email())
            .RuleFor(u => u.Password, faker => faker.Internet.Password(10));
    }
}