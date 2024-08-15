namespace BarberBoss.Communication.DTOs.Request.BarberShopRequests;

/// <summary>
/// Request for registering a new barber shop.
/// </summary>
public record RequestRegisterBarberShopJson
{
    public required string Name { get; set; }
    public required long UserId { get; set; }
}