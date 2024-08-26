using FluentAssertions;
using Moq;

namespace BarberBoss.Infrastructure.Tests.SecurityTests;

public class BCryptTests
{
    private readonly Mock<Security.Cryptography.BCrypt> _bCryptMock;

    public BCryptTests()
    {
        _bCryptMock = new Mock<Security.Cryptography.BCrypt>();
    }

    /// <summary>
    /// Tests if the Encrypt method works
    /// </summary>
    [Fact]
    public void Encrypt_Should_Encrypt_Password()
    {
        var password = "TestPassword123!";
        
        var result = _bCryptMock.Object.Encrypt(password);
        
        result.Should().NotBeEmpty();
        result.Should().BeOfType<string>();
        result.Should().NotBe(password);
    }

    /// <summary>
    /// Tests if the Verify method works with a correct password
    /// </summary>
    [Fact]
    public void Verify_Should_Return_True_For_Valid_Password()
    {
        var password = "TestPassword123!";
        var passwordHash = _bCryptMock.Object.Encrypt(password);
        
        var result = _bCryptMock.Object.Verify(password, passwordHash);
        
        result.Should().BeTrue();
    }

    /// <summary>
    /// Tests if the Verify method works with an incorrect password
    /// </summary>
    [Fact]
    public void Verify_Should_Return_False_For_Incorrect_Password()
    {
        var password = "TestPassword123!";
        var incorrectPassword = "IncorrectPassword!";
        var passwordHash = _bCryptMock.Object.Encrypt(password);
        
        var result = _bCryptMock.Object.Verify(incorrectPassword, passwordHash);
        
        result.Should().BeFalse();
    }
}