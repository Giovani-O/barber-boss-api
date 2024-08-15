using BarberBoss.Domain.Entities;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Tests.Utilities.Requests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.Tests.BarberShopRepositoryTests;

public class BarberShopRepositoryTest
{
    private readonly BarberShop _barberShopA;
    private readonly BarberShop _barberShopB;
    private readonly BarberShop _barberShopC;
    
    private readonly BarberShopRepository _barberShopRepository;

    public BarberShopRepositoryTest()
    {
        _barberShopA = BarberShopBuilder.Build();
        _barberShopB = BarberShopBuilder.Build();
        _barberShopC = BarberShopBuilder.Build();
        List<BarberShop> barberShops = [_barberShopA, _barberShopB, _barberShopC];

        var options = new DbContextOptionsBuilder<BarberBossDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        
        var context = new BarberBossDbContext(options);
        context.BarberShops.AddRange(barberShops);
        context.SaveChanges();

        _barberShopRepository = new BarberShopRepository(context);
    }

    /// <summary>
    /// Tests the GetAllByUserId method to ensure it returns barber shops by user id.
    /// </summary>
    [Fact]
    public async Task GetAllByUserId_ShouldReturnBarberShopsByUserId()
    {
        var result = await _barberShopRepository.GetAllByUserId(_barberShopA.UserId);
        
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.Should().AllBeOfType<BarberShop>();
        result.Should().Contain(b => b.UserId == _barberShopA.UserId);
        result.Should().NotContain(b => b.UserId == _barberShopA.UserId + 1);
    }

    /// <summary>
    /// Tests the GetById method to ensure it returns a barber shop by id.
    /// </summary>
    [Fact]
    public async Task GetById_ShouldReturnBarberShopById()
    {
        var result = await _barberShopRepository.GetById(_barberShopB.Id);
        
        result.Should().NotBeNull();
        result!.Id.Should().Be(_barberShopB.Id);
        result.Name.Should().Be(_barberShopB.Name);
    }

    /// <summary>
    /// Tests the Add method to ensure it adds a barber shop to the database.
    /// </summary>
    [Fact]
    public async Task Add_ShouldAddBarberShopToDatabase()
    {   
        var barberShopD = BarberShopBuilder.Build();
        var result = () => _barberShopRepository.Add(barberShopD);

        await result.Should().NotThrowAsync();
    }

    /// <summary>
    /// Tests the Delete method to ensure it deletes a barber shop by id.
    /// </summary>
    [Fact]
    public async Task Delete_ShouldDeleteBarberShopFromDatabase()
    {
        var result = await _barberShopRepository.Delete(_barberShopC.Id);

        result.Should().BeTrue();
    }
    
    /// <summary>
    /// Tests the Update method to ensure it updates a barber shop in the database.
    /// </summary>
    [Fact]
    public void Update_ShouldUpdateBarberShopInDatabase()
    { 
        var result = () => _barberShopRepository.Update(_barberShopB);
        
        result.Should().NotThrow();
    }

    /// <summary>
    /// Tests the GetById method to ensure it returns a barber shop by id before updating it.
    /// </summary>
    [Fact]
    public async Task Update_GetById_ShouldReturnBarberShopById()
    {
        var result = await _barberShopRepository.GetByIdForUpdate(_barberShopB.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(_barberShopB.Id);
        result.Should().BeOfType<BarberShop>();
        result.Name.Should().Be(_barberShopB.Name);
    }
}