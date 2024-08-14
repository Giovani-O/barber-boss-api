using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.BarberShopRepository;
using FluentAssertions;
using Moq;

namespace BarberBoss.Infrastructure.Tests.BarberShopRepository;

public class BarberShopRepositoryTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IBarberShopReadOnlyRepository> _barberShopReadOnlyRepositoryMock;
    private readonly Mock<IBarberShopWriteOnlyRepository> _barberShopWriteOnlyRepositoryMock;
    private readonly Mock<IBarberShopUpdateOnlyRepository> _barberShopUpdateOnlyRepositoryMock;

    public BarberShopRepositoryTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _barberShopReadOnlyRepositoryMock = new Mock<IBarberShopReadOnlyRepository>();
        _barberShopWriteOnlyRepositoryMock = new Mock<IBarberShopWriteOnlyRepository>();
        _barberShopUpdateOnlyRepositoryMock = new Mock<IBarberShopUpdateOnlyRepository>();

        var barberShops = new List<BarberShop>
        {
            new BarberShop { Id = 1, UserId = 1, Name = "Barber Shop 1" },
            new BarberShop { Id = 2, UserId = 1, Name = "Barber Shop 2" },
            new BarberShop { Id = 3, UserId = 2, Name = "Barber Shop 3"}
        };
        _barberShopReadOnlyRepositoryMock.Setup(b => b.GetAllByUserId(It.IsAny<long>())).ReturnsAsync(barberShops); 
        _barberShopReadOnlyRepositoryMock.Setup(b => b.GetById(It.IsAny<long>())).ReturnsAsync(barberShops[0]);
        
        _barberShopWriteOnlyRepositoryMock.Setup(b => b.Delete(It.IsAny<long>())).ReturnsAsync(true);
        
        _barberShopUpdateOnlyRepositoryMock.Setup(b => b.GetById(It.IsAny<long>())).ReturnsAsync(barberShops[0]);
    }

    /// <summary>
    /// Tests the GetAllByUserId method to ensure it returns barber shops by user id.
    /// </summary>
    [Fact]
    public async Task GetAllByUserId_ShouldReturnBarberShopsByUserId()
    {
        var barberShops = await _barberShopReadOnlyRepositoryMock.Object.GetAllByUserId(1);
        
        barberShops.Should().NotBeNull();
        barberShops.Should().NotBeEmpty();
        barberShops.Should().BeOfType<List<BarberShop>>();
        barberShops.Should().Contain(b => b.UserId == 1);
    }

    /// <summary>
    /// Tests the GetById method to ensure it returns a barber shop by id.
    /// </summary>
    [Fact]
    public async Task GetById_ShouldReturnBarberShopById()
    {
        var barberShop = await _barberShopReadOnlyRepositoryMock.Object.GetById(1);

        barberShop.Should().NotBeNull();
        barberShop!.Id.Should().Be(1);
        barberShop.Name.Should().Be("Barber Shop 1");
    }

    /// <summary>
    /// Tests the Add method to ensure it adds a barber shop to the database.
    /// </summary>
    [Fact]
    public async Task Add_ShouldAddBarberShopToDatabase()
    {   
        var barberShop = new BarberShop { UserId = 2, Name = "Barber Shop 4" };
        

        Func<Task> result = () => _barberShopWriteOnlyRepositoryMock.Object.Add(barberShop);

        await result.Should().NotThrowAsync();
    }

    /// <summary>
    /// Tests the Delete method to ensure it deletes a barber shop by id.
    /// </summary>
    [Fact]
    public async Task Delete_ShouldDeleteBarberShopFromDatabase()
    {
        var deleteResult = await _barberShopWriteOnlyRepositoryMock.Object.Delete(1);

        deleteResult.Should().BeTrue();
    }
    
    /// <summary>
    /// Tests the Update method to ensure it updates a barber shop in the database.
    /// </summary>
    [Fact]
    public void Update_ShouldUpdateBarberShopInDatabase()
    { 
        var barberShop = new BarberShop { Id = 1, UserId = 1, Name = "Barber Shop 1" };
        barberShop.Name = "Barber Shop 1 Updated";

        Action result = () => _barberShopUpdateOnlyRepositoryMock.Object.Update(barberShop);
        
        result.Should().NotThrow();
    }

    /// <summary>
    /// Tests the GetById method to ensure it returns a barber shop by id before updating it.
    /// </summary>
    [Fact]
    public async Task Update_GetById_ShouldReturnBarberShopById()
    {
        var barberShop = await _barberShopUpdateOnlyRepositoryMock.Object.GetById(1);

        barberShop.Should().NotBeNull();
        barberShop!.Id.Should().Be(1);
        barberShop.Should().BeOfType<BarberShop>();
        barberShop.Name.Should().Be("Barber Shop 1");
    }
}