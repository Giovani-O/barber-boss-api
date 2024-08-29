using AutoMapper;
using BarberBoss.Communication.DTOs.Response.UserResponses;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.UserRepository;

namespace BarberBoss.Application.UseCases.Users.GetAll;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllUsersUseCase(
        IUserReadOnlyRepository readOnlyRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _readOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all existing users
    /// </summary>
    /// <returns>IEnumerable of ResponseUserJson</returns>
    public async Task<IEnumerable<ResponseUserJson>> Execute()
    {
        var users = await _readOnlyRepository.GetAll();
        IEnumerable<ResponseUserJson> result = _mapper.Map<IEnumerable<ResponseUserJson>>(users);

        result = result.OrderBy(u => u.Name);
        return result;
    }
}