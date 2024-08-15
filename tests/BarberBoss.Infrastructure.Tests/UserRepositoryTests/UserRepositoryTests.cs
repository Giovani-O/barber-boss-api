using BarberBoss.Domain.Entities;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Tests.Utilities.Requests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.Tests.UserRepositoryTests;

public class UserRepositoryTests
{
    private readonly User _userA;
    private readonly User _userB;
    private readonly User _userC;

    private readonly UserRepository _userRepository;

    public UserRepositoryTests()
    {
        _userA = UserBuilder.Build();
        _userB = UserBuilder.Build();
        _userC = UserBuilder.Build();
        List<User> users = [_userA, _userB, _userC];
        
        var options = new DbContextOptionsBuilder<BarberBossDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var context = new BarberBossDbContext(options);
        context.Users.AddRange(users);
        context.SaveChanges();
        
        _userRepository = new UserRepository(context);
    }
    
    /// <summary>
    /// Tests the GetAll method to ensure it returns all users.
    /// </summary>
    [Fact]
    public async Task GetAll_ShouldReturnAllUsers()
    {
        var getAllUsers = await _userRepository.GetAll();

        getAllUsers.Should().NotBeNull();
        getAllUsers.Should().NotBeEmpty();
        getAllUsers.Should().AllBeOfType<User>();
        getAllUsers.Should().BeOfType<List<User>>();
    }

    /// <summary>
    /// Tests the GetById method to ensure it returns a user by id.
    /// </summary>
    [Fact]
    public async Task GetById_ShouldReturnUserById()
    {
        var result = await _userRepository.GetById(_userA.Id);

        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
        result!.Id.Should().Be(_userA.Id);
    }

    /// <summary>
    /// Tests the Add method to ensure it adds a user to the database.
    /// </summary>
    [Fact]
    public async Task Add_ShouldAddUserToDatabase()
    {
        var userD = UserBuilder.Build();
        var result = () => _userRepository.Add(userD);

        await result.Should().NotThrowAsync();
    }

    /// <summary>
    /// Tests the Delete method to ensure it deletes a user by id.
    /// </summary>
    [Fact]
    public async Task Delete_ShouldDeleteUserFromDatabase()
    {
        var deleteResult = await _userRepository.Delete(_userC.Id);

        deleteResult.Should().BeTrue();
    }

    /// <summary>
    /// Tests the Update method to ensure it updates a user in the database.
    /// </summary>
    [Fact]
    public void Update_ShouldUpdateUserInDatabase()
    {
        var result = () => _userRepository.Update(_userB);
        
        result.Should().NotThrow();
    }

    /// <summary>
    /// Tests the GetById method to ensure it returns a user by id before updating it.
    /// </summary>
    [Fact]
    public async Task Update_GetById_ShouldReturnUserById()
    {   
        var result = await _userRepository.GetById(_userB.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(_userB.Id);
        result.Should().BeOfType<User>();
        result.Name.Should().Be(_userB.Name);
        result.Email.Should().Be(_userB.Email);
    }
}