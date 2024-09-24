using BarberBoss.Communication.DTOs.Response.BarberShopResponses;

namespace BarberBoss.Application.UseCases.BarberShops.GetAll;

public interface IGetAllBarberShopsUseCase
{
    Task<IEnumerable<ResponseBarberShopJson>> Execute(long userId);
}