using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberBoss.Domain.Entities;

public class BarberShop
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public long UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } = default!;
    
    [InverseProperty("BarberShop")]
    public ICollection<Income> Incomes { get; set; } = new List<Income>();
}