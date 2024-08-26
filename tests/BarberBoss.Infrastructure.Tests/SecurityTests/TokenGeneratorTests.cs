using BarberBoss.Infrastructure.Security.Token;
using BarberBoss.Tests.Utilities.Requests;
using BarberBoss.Domain.Security.Token;
using FluentAssertions;
using Moq;

namespace BarberBoss.Infrastructure.Tests.SecurityTests;

public class TokenGeneratorTests
{
    private readonly Mock<JwtTokenGenerator> _tokenGeneratorMock;

    public TokenGeneratorTests(ITokenGenerator tokenGenerator)
    {
        _tokenGeneratorMock = new Mock<JwtTokenGenerator>();
    }

    [Fact]
    public void JwtTokenGenerator_Should_Generate_Token()
    {
        var user = UserBuilder.Build();
        
        var result = _tokenGeneratorMock.Object.Generate(user);

        result.Should().BeOfType<string>();

    }
}