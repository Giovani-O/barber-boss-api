using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.IncomeRepository;
using FluentAssertions;
using Moq;

namespace BarberBoss.Infrastructure.Tests.IncomeRepository;

public class IncomeRepositoryTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IIncomeReadOnlyRepository> _incomeReadOnlyRepositoryMock;
    private readonly Mock<IIncomeWriteOnlyRepository> _incomeWriteOnlyRepositoryMock;
    private readonly Mock<IIncomeUpdateOnlyRepository> _incomeUpdateOnlyRepositoryMock;

    public IncomeRepositoryTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _incomeReadOnlyRepositoryMock = new Mock<IIncomeReadOnlyRepository>();
        _incomeWriteOnlyRepositoryMock = new Mock<IIncomeWriteOnlyRepository>();
        _incomeUpdateOnlyRepositoryMock = new Mock<IIncomeUpdateOnlyRepository>();

        var incomes = new List<Income>()
        {
            new Income
            {
                Id = 1,
                Title = "Serviço 1",
                ServiceDate = DateTime.Now.AddDays(-1),
                PaymentType = "0",
                Price = 50.00m,
                BarberShopId = 1
            },
            new Income
            {
                Id = 2,
                Title = "Serviço 2",
                ServiceDate = DateTime.Now.AddDays(-2),
                PaymentType = "1",
                Price = 30.00m,
                BarberShopId = 1
            },
            new Income
            {
                Id = 3,
                Title = "Serviço 3",
                ServiceDate = DateTime.Now.AddDays(-2),
                PaymentType = "1",
                Price = 70.00m,
                BarberShopId = 2
            }
        };

        _incomeReadOnlyRepositoryMock.Setup(i => i.GetAllByBarberShopId(It.IsAny<long>())).ReturnsAsync(incomes.Where(i => i.BarberShopId == 1).ToList());
        _incomeReadOnlyRepositoryMock.Setup(i => i.GetById(It.IsAny<long>())).ReturnsAsync(incomes.FirstOrDefault(i => i.Id == 1));
        
        _incomeWriteOnlyRepositoryMock.Setup(i => i.Add(It.IsAny<Income>())).Returns(Task.CompletedTask);
        _incomeWriteOnlyRepositoryMock.Setup(i => i.Delete(It.IsAny<long>())).ReturnsAsync(true);

        _incomeUpdateOnlyRepositoryMock.Setup(i => i.GetById(It.IsAny<long>())).ReturnsAsync(incomes[0]);
    }

    /// <summary>
    /// Test the GetAllByBarberShopId method to retrieve incomes by barber shop id.
    /// </summary>
    [Fact]
    public async Task GetAllByBarberShopId_ShouldReturnIncomesByBarberShopId()
    {
        var barberShopId = 1;
        var incomes = await _incomeReadOnlyRepositoryMock.Object.GetAllByBarberShopId(barberShopId);

        incomes.Should().NotBeNull();
        incomes.Should().NotBeEmpty();
        incomes.Should().AllBeOfType<Income>();
        incomes.Should().OnlyContain(i => i.BarberShopId == barberShopId);
    }

    /// <summary>
    /// Test the GetById method to retrieve income by id.
    /// </summary>
    [Fact]
    public async Task GetById_ShouldReturnIncomeById()
    {
        var incomeId = 1;
        var income = await _incomeReadOnlyRepositoryMock.Object.GetById(incomeId);

        income.Should().NotBeNull();
        income!.Id.Should().Be(incomeId);
    }

    /// <summary>
    /// Test the Add method to add an income to the database.
    /// </summary>
    [Fact]
    public async Task Add_ShouldAddIncomeToDatabase()
    {
        var newIncome = new Income
        {
            Title = "Serviço 4",
            ServiceDate = DateTime.Now.AddDays(-1),
            PaymentType = "0",
            Price = 100.00m,
            BarberShopId = 2
        };

        Func<Task> result = async () => await _incomeWriteOnlyRepositoryMock.Object.Add(newIncome);

        await result.Should().NotThrowAsync();
    }

    /// <summary>
    /// Test the Delete method to delete an income from the database.
    /// </summary>
    [Fact]
    public async Task Delete_ShouldDeleteIncomeFromDatabase()
    {   
        var deleteResult = await _incomeWriteOnlyRepositoryMock.Object.Delete(1);

        deleteResult.Should().BeTrue();
    }

    /// <summary>
    /// Test the Update method to update an income in the database.
    /// </summary>
    [Fact]
    public void Update_ShouldUpdateIncomeInDatabase()
    {
        var incomeToUpdate = new Income
        {
            Id = 1,
            Title = "Serviço 1",
            ServiceDate = DateTime.Now.AddDays(-1),
            PaymentType = "0",
            Price = 50.00m,
            BarberShopId = 1
        };
        incomeToUpdate.Title = "Serviço 1 updated";
        
        Action updateResult = () => _incomeUpdateOnlyRepositoryMock.Object.Update(incomeToUpdate);
        
        updateResult.Should().NotThrow();
    }

    /// <summary>
    /// Tests the GetById method to ensure it returns an Income by id before updating it.
    /// </summary>
    [Fact]
    public async Task Update_GetById_ShouldReturnIncomeById()
    {   
        var incomeId = 1;
        var income = await _incomeUpdateOnlyRepositoryMock.Object.GetById(incomeId);

        income.Should().NotBeNull();
        income!.Id.Should().Be(incomeId);
        income.Title.Should().Be("Serviço 1");
    }
}