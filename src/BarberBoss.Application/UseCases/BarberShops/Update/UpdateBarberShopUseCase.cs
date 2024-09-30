using AutoMapper;
using BarberBoss.Communication.DTOs.Request.BarberShopRequests;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.BarberShopRepository;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;
using FluentValidation.Results;

namespace BarberBoss.Application.UseCases.BarberShops.Update;

public class UpdateBarberShopUseCase : IUpdateBarberShopUseCase
{
    private readonly IBarberShopUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBarberShopUseCase(
        IBarberShopUpdateOnlyRepository repository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Updates a barber shop
    /// </summary>
    /// <param name="id">long</param>
    /// <param name="request">RequestUpdateBarberShopJson</param>
    /// <exception cref="NotFoundException">Thrown if the barber shop is not found</exception>
    public async Task Execute(long id, RequestUpdateBarberShopJson request)
    {
        Validate(id, request);
        
        var barberShop = await _repository.GetByIdForUpdate(id);
        if (barberShop is null)
        {
            throw new NotFoundException(new Dictionary<string, List<string>>
            {   
                {nameof(User.Id), [ResourceErrorMessages.BARBER_SHOP_NOT_FOUND] }
            });
        }

        _mapper.Map(request, barberShop);
        _repository.Update(barberShop);
        await _unitOfWork.Commit();
    }

    /// <summary>
    /// Check if update data is valid
    /// </summary>
    /// <param name="id">long</param>
    /// <param name="barberShop"></param>
    /// <exception cref="ErrorOnValidationException"></exception>
    private void Validate(long id, RequestUpdateBarberShopJson barberShop)
    {
        var result = new UpdateBarberShopValidator().Validate(barberShop);

        if (id != barberShop.Id)
        {
            result.Errors.Add(new ValidationFailure(nameof(BarberShop.Id), ResourceErrorMessages.ID_IS_INVALID));
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