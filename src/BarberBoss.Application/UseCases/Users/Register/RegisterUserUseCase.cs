using AutoMapper;
using BarberBoss.Communication.DTOs.Request.UserRequests;
using BarberBoss.Communication.DTOs.Response.UserResponses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.UserRepository;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;
using FluentValidation.Results;

namespace BarberBoss.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordEncrypter _passwordEncrypter;

    public RegisterUserUseCase(
        IUserWriteOnlyRepository writeOnlyRepository,
        IUserReadOnlyRepository readOnlyRepository,
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        IPasswordEncrypter passwordEncrypter)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordEncrypter = passwordEncrypter;
    }
    
    /// <summary>
    /// Use case to register a new user
    /// </summary>
    /// <param name="request">RequestRegisterUserJson</param>
    public async Task<ResponseUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncrypter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();
        
        await _writeOnlyRepository.Add(user);
        await _unitOfWork.Commit();

        return new ResponseUserJson
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }

    /// <summary>
    /// Validate a user creation request
    /// </summary>
    /// <param name="user">RequestRegisterUserJson</param>
    /// <exception cref="ErrorOnValidationException"></exception>
    private async Task Validate(RequestRegisterUserJson user)
    {
        var result = await new RegisterUserValidator().ValidateAsync(user);

        var emailExists = await _readOnlyRepository.CheckIfUserExists(user.Email);
        
        if (emailExists) result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
        
        if (!result.IsValid)
        {
            var errorDictionary = result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(e => e.ErrorMessage).ToList());
            
            throw new ErrorOnValidationException(errorDictionary);
        }
    }
}