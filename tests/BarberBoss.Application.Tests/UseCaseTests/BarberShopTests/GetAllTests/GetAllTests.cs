using AutoMapper;
using BarberBoss.Application.Mappings;
using BarberBoss.Application.UseCases.BarberShops.GetAll;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Tests.Utilities.Requests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BarberBoss.Application.Tests.UseCaseTests.BarberShopTests.GetAllTests;

public class GetAllTests
{
    private readonly GetAllBarberShopsUseCase _useCase;
    private readonly BarberShop _barberShopA;

    public GetAllTests()
    {
        _barberShopA = BarberShopBuilder.Build();
        
        // Creates in memory database
        var options = new DbContextOptionsBuilder<BarberBossDbContext>()
            .UseInMemoryDatabase(databaseName: "GetAllBarberShopsDatabase")
            .Options;

        // Creates fake db context
        var dbContext = new BarberBossDbContext(options);
        dbContext.BarberShops.AddRange(_barberShopA);
        dbContext.SaveChanges();
        
        // Instances used as parameters by the use cae
        var barberShopRepository = new BarberShopRepository(dbContext);
        Mock<IUnitOfWork> unitOfWork = new();
        IMapper mapper = new Mapper(new MapperConfiguration(config =>
        {
            config.AddProfile<BarberShopMappingProfile>();
        }));

        // Use case instance
        _useCase = new GetAllBarberShopsUseCase(
            barberShopRepository, unitOfWork.Object, mapper);
    }

    /// <summary>
    /// Tests if GetAllBarberShops gets all barber shops for a specific user
    /// </summary>
    [Fact]
    public async Task GetAllBarberShops_Should_Return_All_BarberShops()
    {
        var barberShops = await _useCase.Execute(_barberShopA.UserId);
        var response = barberShops.ToList();

        response.Should().HaveCount(1);
        response.Should().Contain(x => x.Name == _barberShopA.Name);
        response.Should().Contain(x => x.Id == _barberShopA.Id);
    }

    /// <summary>
    /// Tests if GetAllBarberShops throws an exception when no barber shops are found
    /// </summary>
    [Fact]
    public async Task GetAllBarberShops_Should_Throw_404_If_Not_Found()
    {
        long userId = 9999;

        if (userId == _barberShopA.UserId) userId++;

        Func<Task> result = () => _useCase.Execute(userId);

        var exception = await result.Should().ThrowAsync<NotFoundException>();
        var errors = exception.Which.GetErrors();
        
        errors.Should().ContainKey(nameof(BarberShop.UserId))
            .WhoseValue.Should().Contain(ResourceErrorMessages.BARBER_SHOPS_NOT_FOUND);
    }
}

