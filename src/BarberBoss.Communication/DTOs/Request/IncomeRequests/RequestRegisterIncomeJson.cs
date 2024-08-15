using BarberBoss.Domain.Enums;

namespace BarberBoss.Communication.DTOs.Request.IncomeRequests;

/// <summary>
/// Request to register a new income.
/// </summary>
public record RequestRegisterIncomeJson
{
    public required string Title { get; set; }
    public string? Description { get; set; } = string.Empty;
    public required DateTime ServiceDate { get; set; }
    public required PaymentType PaymentType { get; set; }
    public required decimal Price { get; set; }
    public required long BarberShopId { get; set; }
}