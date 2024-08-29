using AutoMapper;
using BarberBoss.Application.Mappings;
using BarberBoss.Application.UseCases.Users.Register;
using BarberBoss.Communication.DTOs.Request.UserRequests;
using BarberBoss.Exception;
using BarberBoss.Tests.Utilities;
using BarberBoss.Tests.Utilities.Requests.Tools;
using FluentAssertions;

namespace BarberBoss.Application.Tests.UseCaseTests.UserTests.RegisterTests;

/// <summary>
/// Tests the validation of the user registration request
/// </summary>
public class RegisterValidationTests
{
    private readonly string _testPassword = "TestPassword123!@";
    
    private readonly IMapper _mapper;

    public RegisterValidationTests()
    {
        var mapperConfig = new MapperConfiguration(
            config => config.AddProfile<UserMappingProfile>());
        _mapper = mapperConfig.CreateMapper();
    }
    
    /// <summary>
    /// Tests if the validation is successful
    /// </summary>
    [Fact]
    public void Validation_Is_Successful()
    {
        var validator = new RegisterUserValidator();
        var user = UserBuilder.Build();
        user.Password = _testPassword;
        var request = _mapper.Map<RequestRegisterUserJson>(user);
        
        var validationResult = validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }

    /// <summary>
    /// Tests validation when name is empty
    /// </summary>
    [Fact]
    public void Validation_Name_Is_Empty()
    {
        var validator = new RegisterUserValidator();
        var user = UserBuilder.Build();
        user.Password = _testPassword;
        user.Name = string.Empty;
        var request = _mapper.Map<RequestRegisterUserJson>(user);

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_EMPTY));
    }

    /// <summary>
    /// Tests validation when name is too long
    /// </summary>
    [Fact]
    public void Validation_Name_Is_Too_Long()
    {
        var validator = new RegisterUserValidator();
        var user = UserBuilder.Build();
        user.Password = _testPassword;
        user.Name = StringGenerator.NewString(101);
        var request = _mapper.Map<RequestRegisterUserJson>(user);

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_TOO_LONG));
    }

    /// <summary>
    /// Tests validation when email is empty
    /// </summary>
    [Fact]
    public void Validation_Email_Is_Empty()
    {
        var validator = new RegisterUserValidator();
        var user = UserBuilder.Build();
        user.Password = _testPassword;
        user.Email = string.Empty;
        var request = _mapper.Map<RequestRegisterUserJson>(user);

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_EMPTY));
    }

    /// <summary>
    /// Tests validation when email is invalid
    /// </summary>
    [Fact]
    public void Validation_Email_Is_Invalid()
    {
        var validator = new RegisterUserValidator();
        var user = UserBuilder.Build();
        user.Password = _testPassword;
        user.Email = "invalid_email";
        var request = _mapper.Map<RequestRegisterUserJson>(user);

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_INVALID));
    }

    /// <summary>
    /// Tests validation when email is too long
    /// </summary>
    [Fact]
    public void Validation_Email_Is_Too_Long()
    {
        var validator = new RegisterUserValidator();
        var user = UserBuilder.Build();
        user.Password = _testPassword;
        user.Email = StringGenerator.NewEmail(50);
        var request = _mapper.Map<RequestRegisterUserJson>(user);

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_TOO_LONG));
    }

    /// <summary>
    /// Tests if password validation fails successfuly
    /// </summary>
    /// <param name="password"></param>
    [Theory]
    [InlineData("UPPERCASE123!@")]
    [InlineData("lowercase123!@")]
    [InlineData("NoNumbers!@")]
    [InlineData("NoSpecialCharacters123")]
    [InlineData("ThisPasswordIsTooLongForTheValidationAaBbCc1234567890!@$$%&*AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz")]
    [InlineData("Sh0r?")]
    [InlineData("")]
    [InlineData("     ")]
    public void Validation_Password_Does_Not_Match_Rules(string password)
    {
        var validator = new RegisterUserValidator();
        var user = UserBuilder.Build();
        user.Password = password;
        var request = _mapper.Map<RequestRegisterUserJson>(user);
        
        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(
            e => e.ErrorMessage.Equals(ResourceErrorMessages.PASSWORD_INVALID) ||
                 e.ErrorMessage.Equals(ResourceErrorMessages.PASSWORD_TOO_SHORT) ||
                 e.ErrorMessage.Equals(ResourceErrorMessages.PASSWORD_TOO_LONG)
        );
    }
}