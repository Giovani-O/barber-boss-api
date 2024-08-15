namespace BarberBoss.Communication.DTOs.Response.BarberShopResponses;

/// <summary>
/// Response containing barber shop details.
/// </summary>
public record ResponseBarberShopJson
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public required long UserId { get; set; }
}