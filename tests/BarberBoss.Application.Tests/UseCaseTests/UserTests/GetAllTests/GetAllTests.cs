using AutoMapper;
using BarberBoss.Application.Mappings;
using BarberBoss.Application.UseCases.Users.GetAll;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Entities;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Tests.Utilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BarberBoss.Application.Tests.UseCaseTests.UserTests.GetAllTests;

public class GetAllTests
{
    private readonly GetAllUsersUseCase _useCase;
    private readonly User _userA;
    private readonly User _userB;

    public GetAllTests()
    {
        // Fake data
        _userA = UserBuilder.Build();
        _userB = UserBuilder.Build();

        // Creates in memory database
        var options = new DbContextOptionsBuilder<BarberBossDbContext>()
            .UseInMemoryDatabase(databaseName: "GetAllUsersDatabase")
            .Options;

        // Creates fake db context
        var dbContext = new BarberBossDbContext(options);
        dbContext.Users.AddRange(_userA, _userB);
        dbContext.SaveChanges();
        
        // Instances used as parameters by the use cae
        var userRepository = new UserRepository(dbContext);
        Mock<IUnitOfWork> unitOfWork = new();
        IMapper mapper = new Mapper(new MapperConfiguration(config =>
        {
            config.AddProfile<UserMappingProfile>();
        }));

        // Use case instance
        _useCase = new GetAllUsersUseCase(
            userRepository, unitOfWork.Object, mapper);
    }

    /// <summary>
    /// Tests if the use case returns the users successfully
    /// </summary>
    [Fact]
    public async Task GetAllUsers_Should_Return_All_Users()
    {
        var users = await _useCase.Execute();
        var responseUserJsons = users.ToList();
        
        responseUserJsons.Should().HaveCount(2);
        responseUserJsons.Should().Contain(x => x.Email == _userA.Email);
        responseUserJsons.Should().Contain(x => x.Email == _userB.Email);
    }
}