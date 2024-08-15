using BarberBoss.Domain.Enums;

namespace BarberBoss.Communication.DTOs.Response.IncomeResponses;

/// <summary>
/// Response for an income.
/// </summary>
public record ResponseIncomeJson
{
    public required long Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; } = string.Empty;
    public required DateTime ServiceDate { get; set; }
    public required PaymentType PaymentType { get; set; }
    public required decimal Price { get; set; }
    public required long BarberShopId { get; set; }
}