namespace BarberBoss.Communication.DTOs.Request.UserRequests;

/// <summary>
/// Request for updating user details.
/// </summary>
public record RequestUpdateUserJson
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}