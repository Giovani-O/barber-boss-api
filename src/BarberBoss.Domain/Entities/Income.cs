using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberBoss.Domain.Entities;

public class Income
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime ServiceDate { get; set; }
    public string PaymentType { get; set; } = string.Empty;
    public decimal Price { get; set; }
    
    public long BarberShopId { get; set; }
    public BarberShop BarberShop { get; set; } = default!;
}