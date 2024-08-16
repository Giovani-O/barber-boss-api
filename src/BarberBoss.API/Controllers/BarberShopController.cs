using BarberBoss.Communication.DTOs.Request.BarberShopRequests;
using BarberBoss.Communication.DTOs.Response.BarberShopResponses;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BarberShopController : ControllerBase
{
    /// <summary>
    /// Gets all barber shops for a specific user.
    /// </summary>
    /// <param name="userId">long</param>
    /// <returns>IEnumerable of ResponseBarberShopJson</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ResponseBarberShopJson>>> GetAllByUserId([FromQuery] long userId) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets a barber shop by id.
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>ResponseBarberShopJson</returns>
    [HttpGet("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResponseBarberShopJson>> GetById([FromQuery] long id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Add a new barber shop.
    /// </summary>
    /// <param name="request">RequestRegisterBarberShopJson</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] RequestRegisterBarberShopJson request)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates an existing barber shop.
    /// </summary>
    /// <param name="id">long</param>
    /// <param name="request">RequestUpdateBarberShopJson</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update([FromQuery] long id, [FromBody] RequestUpdateBarberShopJson request)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Delete a barber shop by id.
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