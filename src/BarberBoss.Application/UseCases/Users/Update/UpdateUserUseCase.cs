using AutoMapper;
using BarberBoss.Communication.DTOs.Request.UserRequests;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.UserRepository;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;
using FluentValidation.Results;

namespace BarberBoss.Application.UseCases.Users.Update;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserUseCase(
        IUserUpdateOnlyRepository repository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Updates a user
    /// </summary>
    /// <param name="id">long</param>
    /// <param name="request">RequestUpdateUserJson</param>
    /// <exception cref="NotFoundException">Exception thrown if user is not found</exception>
    public async Task Execute(long id, RequestUpdateUserJson request)
    {
        Validate(id, request);

        var user = await _repository.GetByIdForUpdate(id);
        if (user is null)
        {
            throw new NotFoundException(new Dictionary<string, List<string>>
            {   
                 {nameof(User.Id), [ResourceErrorMessages.USER_NOT_FOUND] }
            });
        }

        _mapper.Map(request, user);
        _repository.Update(user);
        await _unitOfWork.Commit();
    }
    
    /// <summary>
    /// Validates the request data for user update
    /// </summary>
    /// <param name="id">long</param>
    /// <param name="user">RequestUpdateUserJson</param>
    /// <exception cref="ErrorOnValidationException">Exception</exception>
    private static void Validate(long id, RequestUpdateUserJson user)
    {
        var result = new UpdateUserValidator().Validate(user);

        if (id != user.Id)
        {
            result.Errors.Add(new ValidationFailure(nameof(User.Id), ResourceErrorMessages.ID_IS_INVALID));
        }

        if (!result.IsValid)
        {
            var errorDictionary = result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(e => e.ErrorMessage).ToList());
            
            throw new ErrorOnValidationException(errorDictionary);
        }
    }
}