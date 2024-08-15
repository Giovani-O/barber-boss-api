namespace BarberBoss.Communication.DTOs.Request.BarberShopRequests;

/// <summary>
/// Request for updating a barber shop.
/// </summary>
public record RequestUpdateBarberShopJson
{
    public required long Id { get; set; }
    public required string Name { get; set; }
}