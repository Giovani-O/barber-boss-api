using AutoMapper;
using BarberBoss.Application.Mappings;
using BarberBoss.Application.UseCases.Users.GetById;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Tests.Utilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BarberBoss.Application.Tests.UseCaseTests.UserTests.GetByIdTests;

public class GetByIdTests
{
    private readonly GetUserByIdUseCase _useCase;
    private readonly User _userA;
    private readonly User _userB;

    public GetByIdTests()
    {
        // Fake data
        _userA = UserBuilder.Build();
        _userB = UserBuilder.Build();

        // Creates in memory database
        var options = new DbContextOptionsBuilder<BarberBossDbContext>()
            .UseInMemoryDatabase(databaseName: "GetUserByIdDatabase")
            .Options;

        // Creates fake db context
        var dbContext = new BarberBossDbContext(options);
        dbContext.Users.AddRange(_userA, _userB);
        dbContext.SaveChanges();
        
        // Instances used as parameters by the use cae
        var userRepository = new UserRepository(dbContext);
        IMapper mapper = new Mapper(new MapperConfiguration(config =>
        {
            config.AddProfile<UserMappingProfile>();
        }));

        // Use case instance
        _useCase = new GetUserByIdUseCase(
            userRepository, mapper);
    }

    /// <summary>
    /// Tests if GetUserById returns the specified user
    /// </summary>
    [Fact]
    public async Task GetUserById_Should_Return_User()
    {
        var user = await _useCase.Execute(_userB.Id);

        user.Should().NotBeNull();
        user.Id.Should().Be(_userB.Id);
    }
    
    /// <summary>
    /// Tests if GetUserById throws exception when id is invalid
    /// </summary>
    [Fact]
    public async Task GetUserById_Should_Throw_Validation_Exception()
    {
        Func<Task> result = () => _useCase.Execute(-1);

        var exception = await result.Should().ThrowAsync<ErrorOnValidationException>();
        var errors = exception.Which.GetErrors();

        errors.Should().ContainKey(nameof(User.Id))
            .WhoseValue.Should().Contain(ResourceErrorMessages.ID_IS_INVALID);
    }

    /// <summary>
    /// Tests if GetUserById throws exception when user is not found
    /// </summary>
    [Fact]
    public async Task GetUserById_Should_Throw_Not_Found_Exception()
    {
        Func<Task> result = () => _useCase.Execute(999999);

        var exception = await result.Should().ThrowAsync<NotFoundException>();
        var errors = exception.Which.GetErrors();

        errors.Should().ContainKey(nameof(User.Id))
            .WhoseValue.Should().Contain(ResourceErrorMessages.USER_NOT_FOUND);
    }
}