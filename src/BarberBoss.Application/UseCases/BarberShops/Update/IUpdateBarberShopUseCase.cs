using BarberBoss.Communication.DTOs.Request.BarberShopRequests;

namespace BarberBoss.Application.UseCases.BarberShops.Update;

public interface IUpdateBarberShopUseCase
{
    Task Execute(long id, RequestUpdateBarberShopJson request);
}