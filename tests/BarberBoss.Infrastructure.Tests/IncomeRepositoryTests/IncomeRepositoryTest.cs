using BarberBoss.Domain.Entities;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Tests.Utilities.Requests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BarberBoss.Infrastructure.Tests.IncomeRepositoryTests;

public class IncomeRepositoryTest
{
    private readonly Income _incomeA;
    private readonly Income _incomeB;
    private readonly Income _incomeC;

    private readonly IncomeRepository _incomeRepository;

    /*
     *
     * Use this class as an example of how to implement an in memory database for testing.
     * 
     */
    
    public IncomeRepositoryTest()
    {
        // Builds data for testing
        _incomeA = IncomeBuilder.Build();
        _incomeB = IncomeBuilder.Build();
        _incomeC = IncomeBuilder.Build();
        List<Income> incomes = [_incomeA, _incomeB, _incomeC];

        // Creates the test database
        var options = new DbContextOptionsBuilder<BarberBossDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        
        // Creates the context and populate it with test data
        var context = new BarberBossDbContext(options);
        context.Incomes.AddRange(incomes);
        context.SaveChanges();
        
        // Creates the IncomeRepository using the test database context
        _incomeRepository = new IncomeRepository(context);
    }

    /// <summary>
    /// Test the GetAllByBarberShopId method to retrieve incomes by barber shop id.
    /// </summary>
    [Fact]
    public async Task GetAllByBarberShopId_ShouldReturnIncomesByBarberShopId()
    {
        
        var result = await _incomeRepository.GetAllByBarberShopId(_incomeA.BarberShopId);

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.Should().AllBeOfType<Income>();
        result.Should().OnlyContain(i => i.BarberShopId == _incomeA.BarberShopId);
    }

    /// <summary>
    /// Test the GetById method to retrieve income by id.
    /// </summary>
    [Fact]
    public async Task GetById_ShouldReturnIncomeById()
    {
        var result = await _incomeRepository.GetById(_incomeA.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(_incomeA.Id);
    }

    /// <summary>
    /// Test the Add method to add an income to the database.
    /// </summary>
    [Fact]
    public async Task Add_ShouldAddIncomeToDatabase()
    {
        var incomeD = IncomeBuilder.Build();
        var result = async () => await _incomeRepository.Add(incomeD);

        await result.Should().NotThrowAsync();
    }

    /// <summary>
    /// Test the Delete method to delete an income from the database.
    /// </summary>
    [Fact]
    public async Task Delete_ShouldDeleteIncomeFromDatabase()
    {   
        var deleteResult = await _incomeRepository.Delete(_incomeC.Id);

        deleteResult.Should().BeTrue();
    }

    /// <summary>
    /// Test the Update method to update an income in the database.
    /// </summary>
    [Fact]
    public void Update_ShouldUpdateIncomeInDatabase()
    {
        Action updateResult = () => _incomeRepository.Update(_incomeB);
        
        updateResult.Should().NotThrow();
    }

    /// <summary>
    /// Tests the GetById method to ensure it returns an Income by id before updating it.
    /// </summary>
    [Fact]
    public async Task Update_GetById_ShouldReturnIncomeById()
    {   
        var income = await _incomeRepository.GetByIdForUpdate(_incomeB.Id);
        
        income.Should().NotBeNull();
        income!.Id.Should().Be(_incomeB.Id);
        income.Title.Should().Be(_incomeB.Title);
    }
}