using AutoMapper;
using BarberBoss.Application.Mappings;
using BarberBoss.Application.UseCases.BarberShops.GetById;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Tests.Utilities.Requests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Application.Tests.UseCaseTests.BarberShopTests.GetByIdTests;

public class GetByIdTests
{
    private readonly GetBarberShopByIdUseCase _useCase;
    private readonly BarberShop _barberShopA;
    private readonly BarberShop _barberShopB;

    public GetByIdTests()
    {
        // Fake data
        _barberShopA = BarberShopBuilder.Build();
        _barberShopB = BarberShopBuilder.Build();

        // Creates in memory database
        var options = new DbContextOptionsBuilder<BarberBossDbContext>()
            .UseInMemoryDatabase(databaseName: "GetBarberShopByIdDatabase")
            .Options;

        // Creates fake db context
        var dbContext = new BarberBossDbContext(options);
        dbContext.BarberShops.AddRange(_barberShopA, _barberShopB);
        dbContext.SaveChanges();
        
        // Instances used as parameters by the use cae
        var barberShopRepository = new BarberShopRepository(dbContext);
        IMapper mapper = new Mapper(new MapperConfiguration(config =>
        {
            config.AddProfile<BarberShopMappingProfile>();
        }));

        // Use case instance
        _useCase = new GetBarberShopByIdUseCase(
            barberShopRepository, mapper);
    }
    
    /// <summary>
    /// Tests if GetBarberShopById returns the specified barber shop
    /// </summary>
    [Fact]
    public async Task GetBarberShopById_Should_Return_BarberShop()
    {
        var barberShop = await _useCase.Execute(_barberShopB.Id);

        barberShop.Should().NotBeNull();
        barberShop.Id.Should().Be(_barberShopB.Id);
    }
    
    /// <summary>
    /// Tests if GetBarberShopById throws exception when id is invalid
    /// </summary>
    [Fact]
    public async Task GetBarberShopById_Should_Throw_Validation_Exception()
    {
        Func<Task> result = () => _useCase.Execute(-1);

        var exception = await result.Should().ThrowAsync<ErrorOnValidationException>();
        var errors = exception.Which.GetErrors();

        errors.Should().ContainKey(nameof(BarberShop.Id))
            .WhoseValue.Should().Contain(ResourceErrorMessages.ID_IS_INVALID);
    }

    /// <summary>
    /// Tests if GetBarberShopById throws exception when barber shop is not found
    /// </summary>
    [Fact]
    public async Task GetBarberShopById_Should_Throw_Not_Found_Exception()
    {
        var id = 999;
        if (id == _barberShopA.Id || id == _barberShopB.Id) id++;
        
        Func<Task> result = () => _useCase.Execute(id);

        var exception = await result.Should().ThrowAsync<NotFoundException>();
        var errors = exception.Which.GetErrors();

        errors.Should().ContainKey(nameof(BarberShop.Id))
            .WhoseValue.Should().Contain(ResourceErrorMessages.BARBER_SHOP_NOT_FOUND);
    }
}