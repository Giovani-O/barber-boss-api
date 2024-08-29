using AutoMapper;
using BarberBoss.Communication.DTOs.Response.UserResponses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.UserRepository;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Users.GetById;

public class GetUserByIdUseCase : IGetUserByIdUseCase
{
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;

    public GetUserByIdUseCase(
        IUserReadOnlyRepository readOnlyRepository, 
        IMapper mapper)
    {
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Gets a user by id
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>ResponseUserJson</returns>
    /// <exception cref="ErrorOnValidationException">Exception when the id is invalid</exception>
    /// <exception cref="NotFoundException">Exception when the user is not found</exception>
    public async Task<ResponseUserJson> Execute(long id)
    {
        if (id < 0)
        {
            throw new ErrorOnValidationException(new Dictionary<string, List<string>>()
            {
                {nameof(User.Id), [ResourceErrorMessages.ID_IS_INVALID] }
            });
        }
        
        var user = await _readOnlyRepository.GetById(id);

        if (user is null)
        {
            throw new NotFoundException(new Dictionary<string, List<string>>()
            {
                { nameof(User.Id), [ResourceErrorMessages.USER_NOT_FOUND] }
            });
        }

        var response = _mapper.Map<ResponseUserJson>(user);
        return response;
    }
}