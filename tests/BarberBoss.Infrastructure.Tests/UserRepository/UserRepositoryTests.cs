using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.UserRepository;
using FluentAssertions;
using Moq;

namespace BarberBoss.Infrastructure.Tests.UserRepository;

public class UserRepositoryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepositoryMock;
    private readonly Mock<IUserWriteOnlyRepository> _userWriteOnlyRepositoryMock;
    private readonly Mock<IUserUpdateOnlyRepository> _userUpdateOnlyRepositoryMock;

    public UserRepositoryTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userReadOnlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
        _userWriteOnlyRepositoryMock = new Mock<IUserWriteOnlyRepository>();
        _userUpdateOnlyRepositoryMock = new Mock<IUserUpdateOnlyRepository>();

        var users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john.doe@mail.com" },
        };
        _userReadOnlyRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(users);
        _userReadOnlyRepositoryMock.Setup(u => u.GetById(It.IsAny<long>())).ReturnsAsync(users[0]);
        
        _userWriteOnlyRepositoryMock.Setup(u => u.Delete(It.IsAny<long>())).Returns(Task.FromResult(true));
        
        _userUpdateOnlyRepositoryMock.Setup(u => u.GetById(It.IsAny<long>())).ReturnsAsync(users[0]);
        
    }
    
    /// <summary>
    /// Tests the GetAll method to ensure it returns all users.
    /// </summary>
    [Fact]
    public async Task GetAll_ShouldReturnAllUsers()
    {
        var getAllUsers = await _userReadOnlyRepositoryMock.Object.GetAll();

        getAllUsers.Should().NotBeNull();
        getAllUsers.Should().NotBeEmpty();
        getAllUsers.Should().BeOfType<List<User>>();
    }

    /// <summary>
    /// Tests the GetById method to ensure it returns a user by id.
    /// </summary>
    [Fact]
    public async Task GetById_ShouldReturnUserById()
    {
        var getUserById = await _userReadOnlyRepositoryMock.Object.GetById(1);

        getUserById.Should().NotBeNull();
        getUserById!.Id.Should().Be(1);
        getUserById.Name.Should().Be("John Doe");
        getUserById.Email.Should().Be("john.doe@mail.com");
    }

    /// <summary>
    /// Tests the Add method to ensure it adds a user to the database.
    /// </summary>
    [Fact]
    public async Task Add_ShouldAddUserToDatabase()
    {
        var user = new User { Id = 2, Name = "Jane Doe", Email = "jane.doe@mail.com" };
        
        Func<Task> result = () => _userWriteOnlyRepositoryMock.Object.Add(user);

        await result.Should().NotThrowAsync();
    }

    /// <summary>
    /// Tests the Delete method to ensure it deletes a user by id.
    /// </summary>
    [Fact]
    public async Task Delete_ShouldDeleteUserFromDatabase()
    {
        var deleteResult = await _userWriteOnlyRepositoryMock.Object.Delete(1);

        deleteResult.Should().BeTrue();
    }

    /// <summary>
    /// Tests the Update method to ensure it updates a user in the database.
    /// </summary>
    [Fact]
    public void Update_ShouldUpdateUserInDatabase()
    {
        var user = new User { Id = 1, Name = "John Doe", Email = "john.doe@mail.com" };
        user.Name = "Jane Doe";

        Action result = () => _userUpdateOnlyRepositoryMock.Object.Update(user);
        
        result.Should().NotThrow();
    }

    /// <summary>
    /// Tests the GetById method to ensure it returns a user by id before updating it.
    /// </summary>
    [Fact]
    public async Task UpdateUser_GetById_ShouldReturnUserById()
    {   
        var user = await _userUpdateOnlyRepositoryMock.Object.GetById(1);

        user.Should().NotBeNull();
        user!.Id.Should().Be(1);
        user.Should().BeOfType<User>();
        user.Name.Should().Be("John Doe");
        user.Email.Should().Be("john.doe@mail.com");
    }
}