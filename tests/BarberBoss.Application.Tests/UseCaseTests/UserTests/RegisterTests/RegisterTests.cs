using AutoMapper;
using BarberBoss.Application.Mappings;
using BarberBoss.Application.UseCases.Users.Register;
using BarberBoss.Communication.DTOs.Request.UserRequests;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Tests.Utilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BarberBoss.Application.Tests.UseCaseTests.UserTests.RegisterTests;

public class RegisterTests
{
    private const string TestPassword = "TestPassword123!@";

    private readonly Mapper _mapper;
    private readonly RegisterUserUseCase _registerUserUseCase;

    public RegisterTests()
    {
        // Builds data for the context
        var baseUser = UserBuilder.Build();
        baseUser.Password = TestPassword;
        
        // Create in memory database
        var options = new DbContextOptionsBuilder<BarberBossDbContext>()
            .UseInMemoryDatabase(databaseName: "UserRegisterDatabase")
            .Options;

        // Creates new db context and adds data
        var dbContext = new BarberBossDbContext(options);
        dbContext.Users.Add(baseUser);
        dbContext.SaveChanges();

        // Creates instances of everything that's needed for the use case
        var userRepository = new UserRepository(dbContext);
        Mock<IUnitOfWork> unitOfWork = new();
        _mapper = new Mapper(new MapperConfiguration(config =>
        {
            config.AddProfile<UserMappingProfile>();
        }));
        var passwordEncrypter = new Mock<IPasswordEncrypter>().Object;

        // Sets _unitOfWork to use the in memory context
        unitOfWork.Setup(uow => uow.Commit()).Callback(() =>
        {
            dbContext.SaveChangesAsync();
        });

        // Creates a new instance of the use case
        _registerUserUseCase = new RegisterUserUseCase(
            userRepository,
            userRepository,
            unitOfWork.Object,
            _mapper,
            passwordEncrypter);
    }

    /// <summary>
    /// Tests the success of the register user use case
    /// </summary>
    [Fact]
    public async Task User_Register_Should_Be_Successful()
    {
        var newUser = NewUserBuilder.Build();
        newUser.Password = TestPassword;
        var request = _mapper.Map<RequestRegisterUserJson>(newUser);

        var result = await _registerUserUseCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().BeEquivalentTo(newUser.Name);
        result.Email.Should().BeEquivalentTo(newUser.Email);
    }
}