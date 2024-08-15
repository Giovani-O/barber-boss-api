namespace BarberBoss.Communication.DTOs.Request.UserRequests;

/// <summary>
/// Request for registering a new user.
/// </summary>
public record RequestRegisterUserJson
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}