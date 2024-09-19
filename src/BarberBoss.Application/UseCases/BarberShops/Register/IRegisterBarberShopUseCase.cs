using BarberBoss.Communication.DTOs.Request.BarberShopRequests;
using BarberBoss.Communication.DTOs.Response.BarberShopResponses;

namespace BarberBoss.Application.UseCases.BarberShops.Register;

public interface IRegisterBarberShopUseCase
{
    Task<ResponseBarberShopJson> Execute(RequestRegisterBarberShopJson request);
}