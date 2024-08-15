using AutoMapper;
using BarberBoss.Application.Mappings;
using BarberBoss.Communication.DTOs.Request.UserRequests;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.UserRepository;
using BarberBoss.Tests.Utilities.Requests;
using Bogus;
using FluentAssertions;
using Moq;

namespace BarberBoss.Infrastructure.Tests.UserRepository;

public class UserRepositoryTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepositoryMock;
    private readonly Mock<IUserWriteOnlyRepository> _userWriteOnlyRepositoryMock;
    private readonly Mock<IUserUpdateOnlyRepository> _userUpdateOnlyRepositoryMock;
    private readonly IMapper _mapper;

    public UserRepositoryTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userReadOnlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
        _userWriteOnlyRepositoryMock = new Mock<IUserWriteOnlyRepository>();
        _userUpdateOnlyRepositoryMock = new Mock<IUserUpdateOnlyRepository>();
        
        // AutoMapper
        var mapperConfig = new MapperConfiguration(
            config =>
            {
                config.AddProfile<UserMappingProfile>();
            });
        _mapper = mapperConfig.CreateMapper();
        
        var users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john.doe@mail.com", Password = "1234a!" },
        };
        _userReadOnlyRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(users);
        
        _userWriteOnlyRepositoryMock.Setup(u => u.Delete(It.IsAny<long>())).Returns(Task.FromResult(true));
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
        var user = UserRequestBuilder.Build();
        var response = _mapper.Map<User>(user);
        _userReadOnlyRepositoryMock.Setup(u => u.GetById(It.IsAny<long>())).ReturnsAsync(response);
        var result = await _userReadOnlyRepositoryMock.Object.GetById(user.Id);

        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
        result!.Id.Should().Be(response.Id);
    }

    /// <summary>
    /// Tests the Add method to ensure it adds a user to the database.
    /// </summary>
    [Fact]
    public async Task Add_ShouldAddUserToDatabase()
    {
        var user = NewUserRequestBuilder.Build();
        var request = _mapper.Map<User>(user);
        
        Func<Task> result = () => _userWriteOnlyRepositoryMock.Object.Add(request);

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
        var user = UserRequestBuilder.Build();
        var request = _mapper.Map<User>(user);

        Action result = () => _userUpdateOnlyRepositoryMock.Object.Update(request);
        
        result.Should().NotThrow();
    }

    /// <summary>
    /// Tests the GetById method to ensure it returns a user by id before updating it.
    /// </summary>
    [Fact]
    public async Task Update_GetById_ShouldReturnUserById()
    {   
        var user = UserRequestBuilder.Build();
        var request = _mapper.Map<User>(user);
        _userUpdateOnlyRepositoryMock.Setup(u => u.GetById(It.IsAny<long>())).ReturnsAsync(request);
        
        var result = await _userUpdateOnlyRepositoryMock.Object.GetById(request.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(request.Id);
        result.Should().BeOfType<User>();
        result.Name.Should().Be(request.Name);
        result.Email.Should().Be(request.Email);
    }
}