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
    public async Task<ActionResult<IEnumerable<ResponseUserJson>>> GetAll()
    {
        throw new NotImplementedException();
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
    public async Task<ActionResult<ResponseUserJson>> GetById([FromQuery] long id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Add a new user.
    /// </summary>
    /// <param name="request">RequestRegisterUserJson</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] RequestRegisterUserJson request)
    {
        throw new NotImplementedException();
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