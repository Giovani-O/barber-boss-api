using BarberBoss.Communication.DTOs.Request.IncomeRequests;
using BarberBoss.Communication.DTOs.Response.IncomeResponses;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncomeController : ControllerBase
{
    /// <summary>
    /// Gets all incomes for a specific barber shop.
    /// </summary>
    /// <param name="barberShopId">long</param>
    /// <returns>IEnumerable of ResponseIncomeJson</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ResponseIncomeJson>>> GetAllByBarberShopId([FromQuery] long barberShopId) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets an income by id.
    /// </summary>
    /// <param name="id">long</param>
    /// <returns>ResponseIncomeJson</returns>
    [HttpGet("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResponseIncomeJson>> GetById([FromQuery] long id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Add a new income.
    /// </summary>
    /// <param name="request">RequestRegisterIncomeJson</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] RequestRegisterIncomeJson request)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates an existing income.
    /// </summary>
    /// <param name="id">long</param>
    /// <param name="request">RequestUpdateIncomeJson</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update([FromQuery] long id, [FromBody] RequestUpdateIncomeJson request)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Delete an income by id.
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