using AutoMapper;
using BarberBoss.Communication.DTOs.Response.BarberShopResponses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.BarberShopRepository;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.BarberShops.GetAll;

public class GetAllBarberShopsUseCase : IGetAllBarberShopsUseCase
{
    private readonly IBarberShopReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;

    public GetAllBarberShopsUseCase(
        IBarberShopReadOnlyRepository readOnlyRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {   
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all barber shops for a given user
    /// </summary>
    /// <param name="userId">long</param>
    /// <returns>IEnumerable of ResponseBarberShopJson</returns>
    public async Task<IEnumerable<ResponseBarberShopJson>> Execute(long userId)
    {
        var barberShops = await _readOnlyRepository.GetAllByUserId(userId);

        if (barberShops.Count == 0)
        {
            throw new NotFoundException(new Dictionary<string, List<string>>()
            {
                { nameof(BarberShop.UserId), [ResourceErrorMessages.BARBER_SHOPS_NOT_FOUND] }
            });
        } 
        
        IEnumerable<ResponseBarberShopJson> result = _mapper.Map<IEnumerable<ResponseBarberShopJson>>(barberShops);

        result = result.OrderBy(b => b.Name);
        return result;
    }
}