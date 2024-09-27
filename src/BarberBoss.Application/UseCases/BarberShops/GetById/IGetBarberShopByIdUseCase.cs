using BarberBoss.Communication.DTOs.Response.BarberShopResponses;

namespace BarberBoss.Application.UseCases.BarberShops.GetById;

public interface IGetBarberShopByIdUseCase
{
    Task<ResponseBarberShopJson> Execute(long id);
}