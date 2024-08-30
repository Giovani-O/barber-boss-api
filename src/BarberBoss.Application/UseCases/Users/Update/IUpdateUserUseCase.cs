using BarberBoss.Communication.DTOs.Request.UserRequests;

namespace BarberBoss.Application.UseCases.Users.Update;

public interface IUpdateUserUseCase
{
    Task Execute(long id, RequestUpdateUserJson request);
}