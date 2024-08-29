using BarberBoss.Application.UseCases.Users.GetAll;
using BarberBoss.Application.UseCases.Users.GetById;
using BarberBoss.Application.UseCases.Users.Register;
using BarberBoss.Communication.DTOs.Request.UserRequests;
using BarberBoss.Communication.DTOs.Response.UserResponses;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <returns>IEnumerable of ResponseUserJson</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ResponseUserJson>>> GetAll([FromServices] IGetAllUsersUseCase useCase)
    {
        var response = await useCase.Execute();
        
        return Ok(response);
    }

    /// <summary>
    /// Gets a user by id.
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>ResponseuserJson</returns>
    [HttpGet("GetUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResponseUserJson>> GetById([FromServices] IGetUserByIdUseCase useCase, [FromQuery] long id)
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    /// <summary>
    /// Add a new user
    /// </summary>
    /// <param name="userUseCase">IRegisterUseCase</param>
    /// <param name="request">RequestRegisterUserJson</param>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(
        [FromServices] IRegisterUserUseCase useCase, 
        [FromBody] RequestRegisterUserJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">long</param>
    /// <param name="request">RequestUpdateUserJson</param>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut]
    public async Task<ActionResult> Update([FromQuery] long id, [FromBody] RequestUpdateUserJson request)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Delete a user by id.
    /// </summary>
    /// <param name="id">long</param>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete([FromQuery] long id)
    {
        throw new NotImplementedException();
    }
    
}