using BarberBoss.Application.UseCases.BarberShops.Delete;
using BarberBoss.Application.UseCases.BarberShops.GetAll;
using BarberBoss.Application.UseCases.BarberShops.GetById;
using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Application.UseCases.BarberShops.Update;
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
    /// <param name="useCase">IGetBarberShopByIdUseCase</param>
    /// <param name="id">long</param>
    /// <returns>ResponseBarberShopJson</returns>
    [HttpGet("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResponseBarberShopJson>> GetById(
        [FromServices] IGetBarberShopByIdUseCase useCase,
        [FromQuery] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
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
    /// <param name="useCase">IUpdateBarberShopUseCase</param>
    /// <param name="id">long</param>
    /// <param name="request">RequestUpdateBarberShopJson</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(
        [FromServices] IUpdateBarberShopUseCase useCase,
        [FromQuery] long id, 
        [FromBody] RequestUpdateBarberShopJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    /// <summary>
    /// Delete a barber shop by id.
    /// </summary>
    /// <param name="useCase">IDeleteBarberShopUseCase</param>
    /// <param name="id">long</param>
    /// <returns>NoContent</returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(
        [FromServices] IDeleteBarberShopUseCase useCase,
        [FromQuery] long id)
    {
        await useCase.Execute(id);

        return NoContent();
    }
}