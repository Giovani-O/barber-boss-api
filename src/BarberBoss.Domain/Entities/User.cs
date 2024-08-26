using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberBoss.Domain.Entities;

public class User
{
    [Key]
    public long Id { get; set; }
    [Required]
    public Guid UserIdentifier { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;
    
    [InverseProperty("User")]
    public ICollection<BarberShop> BarberShops { get; set; } = new List<BarberShop>();
    
}