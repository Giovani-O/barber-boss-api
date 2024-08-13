using System.ComponentModel.DataAnnotations.Schema;

namespace BarberBoss.Domain.Entities;

public class BarberShop
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    [ForeignKey("UserId")]
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    
    public ICollection<Income> Services { get; set; } = new List<Income>();
}