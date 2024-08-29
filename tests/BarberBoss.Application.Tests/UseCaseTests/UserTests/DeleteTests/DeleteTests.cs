using AutoMapper;
using BarberBoss.Application.UseCases.Users.Delete;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.UserRepository;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Tests.Utilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BarberBoss.Application.Tests.UseCaseTests.UserTests.DeleteTests;

public class DeleteTests
{
    private readonly DeleteUserUseCase _useCase;
    private readonly UserRepository _userRepository;
    private readonly User _userA;
    private readonly User _userB;

    public DeleteTests()
    {
        // Fake data
        _userA = UserBuilder.Build();
        _userB = UserBuilder.Build();

        // Creates in memory database
        var options = new DbContextOptionsBuilder<BarberBossDbContext>()
            .UseInMemoryDatabase(databaseName: "DeleteDatabase")
            .Options;

        // Creates fake db context
        var dbContext = new BarberBossDbContext(options);
        dbContext.Users.AddRange(_userA, _userB);
        dbContext.SaveChanges();
        
        // Instances used as parameters by the use case
        _userRepository = new UserRepository(dbContext);
        Mock<IUnitOfWork> unitOfWork = new();
        
        // Sets _unitOfWork to use the in memory context
        unitOfWork.Setup(uow => uow.Commit()).Callback(() =>
        {
            dbContext.SaveChangesAsync();
        });

        // Use case instance
        _useCase = new DeleteUserUseCase(_userRepository, _userRepository, unitOfWork.Object);
    }

    /// <summary>
    /// Tests if Delete is successful
    /// </summary>
    [Fact]
    public async Task Delete_User_Should_Be_Successful()
    {
        await _useCase.Execute(_userA.Id);

        var retrievedUser = await _userRepository.GetById(_userA.Id);
        retrievedUser.Should().BeNull();
    }

    /// <summary>
    /// Tests if Delete throws exception when user is not found
    /// </summary>
    [Fact]
    public async Task Delete_Should_Throw_Not_Found_Exception()
    {
        Func<Task> result = () => _useCase.Execute(999999);

        var exception = await result.Should().ThrowAsync<NotFoundException>();
        var errors = exception.Which.GetErrors();

        errors.Should().ContainKey(nameof(User.Id))
            .WhoseValue.Should().Contain(ResourceErrorMessages.USER_NOT_FOUND);
    }

    // [Fact]
    // public async Task Delete_Should_Throw_Error_On_Execution_Exception()
    // {
    //     
    //     
    //     Func<Task> result = () => _useCase.Execute(_userB.Id);
    //
    //     var exception = await result.Should().ThrowAsync<ErrorOnExecution>();
    //     var errors = exception.Which.GetErrors();
    //
    //     errors.Should().ContainKey(nameof(User.Id))
    //         .WhoseValue.Should().Contain(ResourceErrorMessages.UNKNOWN_ERROR);
    // }
}