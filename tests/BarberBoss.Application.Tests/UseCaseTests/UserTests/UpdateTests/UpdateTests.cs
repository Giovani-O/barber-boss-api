using AutoMapper;
using BarberBoss.Application.Mappings;
using BarberBoss.Application.UseCases.Users.Update;
using BarberBoss.Communication.DTOs.Request.UserRequests;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Exception.ExceptionBase;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Tests.Utilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BarberBoss.Application.Tests.UseCaseTests.UserTests.UpdateTests;

public class UpdateTests
{
    private const string TestPassword = "TestPassword123!@";
    
    private readonly Mapper _mapper;
    private readonly UpdateUserUseCase _updateUserUseCase;
    private readonly UserRepository _userRepository;
    private readonly User _userA;
    

    public UpdateTests()
    {
        // Builds data for the context
        _userA = UserBuilder.Build();
        _userA.Password = TestPassword;
        
        // Create in memory database
        var options = new DbContextOptionsBuilder<BarberBossDbContext>()
            .UseInMemoryDatabase(databaseName: "UserUpdateDatabase")
            .Options;

        // Creates new db context and adds data
        var dbContext = new BarberBossDbContext(options);
        dbContext.Users.Add(_userA);
        dbContext.SaveChanges();

        // Creates instances of everything that's needed for the use case
        _userRepository = new UserRepository(dbContext);
        Mock<IUnitOfWork> unitOfWork = new();
        _mapper = new Mapper(new MapperConfiguration(config =>
        {
            config.AddProfile<UserMappingProfile>();
        }));

        // Sets _unitOfWork to use the in memory context
        unitOfWork.Setup(uow => uow.Commit()).Callback(() =>
        {
            dbContext.SaveChangesAsync();
        });

        // Creates a new instance of the use case
        _updateUserUseCase = new UpdateUserUseCase(
            _userRepository,
            unitOfWork.Object,
            _mapper
        );
    }
    
    /// <summary>
    /// Tests the success of the update user use case
    /// </summary>
    [Fact]
    public async Task User_Update_Should_Be_Successful()
    {
        _userA.Name = "Edited";
        var request = _mapper.Map<RequestUpdateUserJson>(_userA);

        Func<Task> result = () => _updateUserUseCase.Execute(_userA.Id, request);
        await result();
        
        var updatedUser = await _userRepository.GetById(_userA.Id);
        
        await result.Should().NotThrowAsync<NotFoundException>();
        await result.Should().NotThrowAsync<ErrorOnValidationException>();
        await result.Should().NotThrowAsync();
        updatedUser!.Name.Should().Be(request.Name);
    }
}