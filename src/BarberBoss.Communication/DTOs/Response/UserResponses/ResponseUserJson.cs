namespace BarberBoss.Communication.DTOs.Response.UserResponses;

/// <summary>
/// Response containing user details.
/// </summary>
public record ResponseUserJson
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}