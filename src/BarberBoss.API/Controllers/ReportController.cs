using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    /// <summary>
    /// Generates an Excel report for a specific barber shop.
    /// </summary>
    /// <param name="barberShopId">long</param>
    [HttpGet("Excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GenerateExcelReport([FromQuery] long barberShopId)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Generates a PDF report for a specific barber shop.
    /// </summary>
    /// <param name="barberShopId">long</param>
    [HttpGet("Pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GeneratePdfReport([FromQuery] long barberShopId)
    {
        throw new NotImplementedException();
    }
}