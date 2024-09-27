using AutoMapper;
using BarberBoss.Communication.DTOs.Response.BarberShopResponses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.BarberShopRepository;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.BarberShops.GetById;

public class GetBarberShopByIdUseCase : IGetBarberShopByIdUseCase
{
    private readonly IBarberShopReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;

    public GetBarberShopByIdUseCase(IBarberShopReadOnlyRepository readOnlyRepository, IMapper mapper)
    {
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Gets a barber shop by its id
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>ResponseBarberShopJson</returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<ResponseBarberShopJson> Execute(long id)
    {
        var barberShop = await _readOnlyRepository.GetById(id);

        if (barberShop is null)
        {
            throw new NotFoundException(new Dictionary<string, List<string>>()
            {
                {nameof(BarberShop.Id), [ResourceErrorMessages.BARBER_SHOP_NOT_FOUND] }
            });
        }

        var response = _mapper.Map<ResponseBarberShopJson>(barberShop);
        return response;
    }
}