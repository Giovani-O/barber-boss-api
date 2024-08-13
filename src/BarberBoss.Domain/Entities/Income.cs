using System.ComponentModel.DataAnnotations.Schema;

namespace BarberBoss.Domain.Entities;

public class Income
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime ServiceDate { get; set; }
    public string PaymentType { get; set; } = string.Empty;
    public decimal Price { get; set; }
    
    [ForeignKey("BarberShopId")]
    public Guid BarberShopId { get; set; }
    public BarberShop BarberShop { get; set; } = default!;
}