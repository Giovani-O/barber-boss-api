using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.BarberShopRepository;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.BarberShops.Delete;

public class DeleteBarberShopUseCase : IDeleteBarberShopUseCase
{
    private readonly IBarberShopReadOnlyRepository _readOnlyRepository;
    private readonly IBarberShopWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBarberShopUseCase(
        IBarberShopReadOnlyRepository readOnlyRepository,
        IBarberShopWriteOnlyRepository writeOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _readOnlyRepository = readOnlyRepository;
        _writeOnlyRepository = writeOnlyRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Execute(long id)
    {
        var barberShop = await _readOnlyRepository.GetById(id);
        if (barberShop is null)
        {
            throw new NotFoundException(new Dictionary<string, List<string>>
            {
                {nameof(BarberShop.Id), [ResourceErrorMessages.BARBER_SHOP_NOT_FOUND] }
            });
        }

        var response = await _writeOnlyRepository.Delete(barberShop.Id);

        if (!response)
        {
            throw new ErrorOnExecution(new Dictionary<string, List<string>>
            {
                {nameof(User.Id), [ResourceErrorMessages.UNKNOWN_ERROR] }
            });
        }
        
        await _unitOfWork.Commit();
    }
}