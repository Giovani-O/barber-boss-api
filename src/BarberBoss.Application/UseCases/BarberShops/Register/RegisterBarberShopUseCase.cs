using AutoMapper;
using BarberBoss.Communication.DTOs.Request.BarberShopRequests;
using BarberBoss.Communication.DTOs.Response.BarberShopResponses;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.BarberShopRepository;

namespace BarberBoss.Application.UseCases.BarberShops.Register;

public class RegisterBarberShopUseCase : IRegisterBarberShopUseCase
{
    private readonly IBarberShopWriteOnlyRepository _writeOnlyRepository;
    private readonly IBarberShopReadOnlyRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public RegisterBarberShopUseCase(
        IBarberShopWriteOnlyRepository writeOnlyRepository,
        IBarberShopReadOnlyRepository readOnlyRepository,
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {   
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Use case to register a new barber shop
    /// </summary>
    /// <param name="request">RequestRegisterBarberShopJson</param>
    /// <returns>ResponseBarberShopJson</returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<ResponseBarberShopJson> Execute(RequestRegisterBarberShopJson request)
    {
        Validate(request);
        
        throw new NotImplementedException();
    }

    /// <summary>
    /// Validate a barber shop creation request
    /// </summary>
    /// <param name="request"></param>
    public void Validate(RequestRegisterBarberShopJson request)
    {
        
    }
}