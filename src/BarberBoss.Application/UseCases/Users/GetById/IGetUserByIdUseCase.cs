using BarberBoss.Communication.DTOs.Response.UserResponses;

namespace BarberBoss.Application.UseCases.Users.GetById;

public interface IGetUserByIdUseCase
{
    Task<ResponseUserJson> Execute(long id);
}