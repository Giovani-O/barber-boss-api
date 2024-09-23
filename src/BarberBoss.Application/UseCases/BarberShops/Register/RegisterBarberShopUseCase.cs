using AutoMapper;
using BarberBoss.Communication.DTOs.Request.BarberShopRequests;
using BarberBoss.Communication.DTOs.Response.BarberShopResponses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.BarberShopRepository;
using BarberBoss.Exception;
using FluentValidation;
using FluentValidation.Results;

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
    public async Task<ResponseBarberShopJson> Execute(RequestRegisterBarberShopJson request)
    {
        await Validate(request);

        var barberShop = _mapper.Map<BarberShop>(request);

        await _writeOnlyRepository.Add(barberShop);
        await _unitOfWork.Commit();

        return new ResponseBarberShopJson
        {
            Id = barberShop.Id,
            Name = barberShop.Name,
            UserId = barberShop.UserId
        };
    }

    /// <summary>
    /// Validate a barber shop creation request
    /// </summary>
    /// <param name="request"></param>
    private async Task Validate(RequestRegisterBarberShopJson request)
    {
        var result = await new RegisterBarberShopValidator().ValidateAsync(request);

        var shopNameExists = await _readOnlyRepository.CheckIfBarberShopExists(request.Name);

        if (shopNameExists)
            result.Errors.Add(new ValidationFailure(string.Empty,
                ResourceErrorMessages.BARBER_SHOP_WITH_THIS_NAME_ALREADY_EXISTS));

        if (!result.IsValid)
        {
            var errorDictionary = result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(e => e.ErrorMessage).ToList());
        }
    }
}