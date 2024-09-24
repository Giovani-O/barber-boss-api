using BarberBoss.Application.UseCases.BarberShops.GetAll;
using BarberBoss.Application.UseCases.BarberShops.Register;
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
    /// <param name="useCase">IGetAllBarberShopsUseCase</param>
    /// <param name="userId">long</param>
    /// <returns>IEnumerable of ResponseBarberShopJson</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ResponseBarberShopJson>>> GetAllByUserId(
        [FromServices] IGetAllBarberShopsUseCase useCase,
        [FromQuery] long userId)
    {
        var response = await useCase.Execute(userId);

        return Ok(response);
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
    /// <param name="useCase">RegisterBarberShopUseCase</param>
    /// <param name="request">RequestRegisterBarberShopJson</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(
        [FromServices] IRegisterBarberShopUseCase useCase, 
        [FromBody] RequestRegisterBarberShopJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
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