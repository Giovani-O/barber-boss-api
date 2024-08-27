using AutoMapper;
using BarberBoss.Application.Mappings;
using BarberBoss.Application.UseCases.Users.Register;
using BarberBoss.Communication.DTOs.Request.UserRequests;
using BarberBoss.Tests.Utilities.Requests;
using FluentAssertions;

namespace BarberBoss.Application.Tests.UseCaseTests.User;

/// <summary>
/// Tests the validation of the user registration request
/// </summary>
public class RegisterValidationTests
{
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
        user.Password = "TestPassword123!@";
        var request = _mapper.Map<RequestRegisterUserJson>(user);
        
        var validationResult = validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
}