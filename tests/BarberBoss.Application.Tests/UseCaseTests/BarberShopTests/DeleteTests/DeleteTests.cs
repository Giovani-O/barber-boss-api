using BarberBoss.Application.UseCases.BarberShops.Delete;
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

namespace BarberBoss.Application.Tests.UseCaseTests.BarberShopTests.DeleteTests;

public class DeleteTests
{
    private readonly DeleteBarberShopUseCase _useCase;
    private readonly BarberShopRepository _barberShopRepository;
    private readonly BarberShop _barberShopA;
    private readonly BarberShop _barberShopB;

    public DeleteTests()
    {
        // Fake data
        _barberShopA = BarberShopBuilder.Build();
        _barberShopB = BarberShopBuilder.Build();

        // Creates in memory database
        var options = new DbContextOptionsBuilder<BarberBossDbContext>()
            .UseInMemoryDatabase(databaseName: "DeleteBarberShopDatabase")
            .Options;

        // Creates fake db context
        var dbContext = new BarberBossDbContext(options);
        dbContext.BarberShops.AddRange(_barberShopA, _barberShopB);
        dbContext.SaveChanges();
        
        // Instances used as parameters by the use case
        _barberShopRepository = new BarberShopRepository(dbContext);
        Mock<IUnitOfWork> unitOfWork = new();
            
        
        // Sets _unitOfWork to use the in memory context
        unitOfWork.Setup(uow => uow.Commit()).Callback(() =>
        {
            dbContext.SaveChangesAsync();
        });

        // Use case instance
        _useCase = new DeleteBarberShopUseCase(_barberShopRepository, _barberShopRepository, unitOfWork.Object);
    }

    /// <summary>
    /// Tests if Delete is successful
    /// </summary>
    [Fact]
    public async Task Delete_User_Should_Be_Successful()
    {
        await _useCase.Execute(_barberShopA.Id);

        var retrievedBarberShop = await _barberShopRepository.GetById(_barberShopA.Id);
        retrievedBarberShop.Should().BeNull();
    }

    /// <summary>
    /// Tests if Delete throws exception when barber shop is not found
    /// </summary>
    [Fact]
    public async Task Delete_Should_Throw_Not_Found_Exception()
    {
        var id = 9999;
        if (id == _barberShopA.Id || id == _barberShopB.Id) { id++; }
        
        Func<Task> result = () => _useCase.Execute(id);

        var exception = await result.Should().ThrowAsync<NotFoundException>();
        var errors = exception.Which.GetErrors();
        
        errors.Should().ContainKey(nameof(BarberShop.Id))
            .WhoseValue.Should().Contain(ResourceErrorMessages.BARBER_SHOP_NOT_FOUND);
    }
}