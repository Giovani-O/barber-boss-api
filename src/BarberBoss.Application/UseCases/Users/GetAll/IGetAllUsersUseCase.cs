using BarberBoss.Communication.DTOs.Response.UserResponses;

namespace BarberBoss.Application.UseCases.Users.GetAll;

public interface IGetAllUsersUseCase
{
    Task<IEnumerable<ResponseUserJson>> Execute();
}