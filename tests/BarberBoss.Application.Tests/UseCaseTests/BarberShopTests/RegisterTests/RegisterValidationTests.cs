using AutoMapper;
using BarberBoss.Application.Mappings;
using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Communication.DTOs.Request.BarberShopRequests;
using BarberBoss.Exception;
using BarberBoss.Tests.Utilities.Requests;
using BarberBoss.Tests.Utilities.Requests.Tools;
using FluentAssertions;

namespace BarberBoss.Application.Tests.UseCaseTests.BarberShopTests.RegisterTests;

/// <summary>
/// Tests the validations when registering a barber shop
/// </summary>
public class RegisterValidationTests
{
    private readonly IMapper _mapper;
    
    public RegisterValidationTests()
    {
        var mapperConfig = new MapperConfiguration(
            config => config.AddProfile<BarberShopMappingProfile>());
        _mapper = mapperConfig.CreateMapper();
    }

    /// <summary>
    /// Tests if the validation is successful
    /// </summary>
    [Fact]
    public void Validation_Is_Successful()
    {
        var validator = new RegisterBarberShopValidator();
        var barberShop = BarberShopBuilder.Build();
        var request = _mapper.Map<RequestRegisterBarberShopJson>(barberShop);
        
        var validationResult = validator.Validate(request);
        
        validationResult.IsValid.Should().BeTrue();
    }
    
    /// <summary>
    /// Tests validation when name is empty
    /// </summary>
    [Fact]
    public void Validation_Name_Is_Empty()
    {
        var validator = new RegisterBarberShopValidator();
        var barberShop = BarberShopBuilder.Build();
        barberShop.Name = string.Empty;
        var request = _mapper.Map<RequestRegisterBarberShopJson>(barberShop);

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
        var validator = new RegisterBarberShopValidator();
        var barberShop = BarberShopBuilder.Build();
        barberShop.Name = StringGenerator.NewString(101);
        var request = _mapper.Map<RequestRegisterBarberShopJson>(barberShop);

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NAME_TOO_LONG));
    }
    
    /// <summary>
    /// Tests validation when user id is invalid
    /// </summary>
    [Fact]
    public void Validation_Id_Is_Invalid()
    {
        var validator = new RegisterBarberShopValidator();
        var barberShop = BarberShopBuilder.Build();
        barberShop.UserId = -1;
        var request = _mapper.Map<RequestRegisterBarberShopJson>(barberShop);

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.ID_IS_INVALID));
    }
}