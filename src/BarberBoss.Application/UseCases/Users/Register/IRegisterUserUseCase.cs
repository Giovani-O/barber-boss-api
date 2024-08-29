using BarberBoss.Communication.DTOs.Request.UserRequests;
using BarberBoss.Communication.DTOs.Response.UserResponses;

namespace BarberBoss.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseUserJson> Execute(RequestRegisterUserJson user);
}