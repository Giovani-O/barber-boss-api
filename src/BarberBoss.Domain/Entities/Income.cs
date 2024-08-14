using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberBoss.Domain.Entities;

public class Income
{
    [Key]
    public long Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string ClientName { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(300)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public DateTime ServiceDate { get; set; }
    [Required]
    public string PaymentType { get; set; } = string.Empty;
    [Required]
    public decimal Price { get; set; }
    
    public long BarberShopId { get; set; }
    [ForeignKey("BarberShopId")]
    public BarberShop BarberShop { get; set; } = default!;
}