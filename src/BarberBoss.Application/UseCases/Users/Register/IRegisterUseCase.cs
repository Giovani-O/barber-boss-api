using BarberBoss.Communication.DTOs.Request.UserRequests;
using BarberBoss.Communication.DTOs.Response.UserResponses;

namespace BarberBoss.Application.UseCases.Users.Register;

public interface IRegisterUseCase
{
    Task<ResponseUserJson> Execute(RequestRegisterUserJson user);
}