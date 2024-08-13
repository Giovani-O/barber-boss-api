using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberBoss.Domain.Entities;

public class BarberShop
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    

    public long UserId { get; set; }
    public User User { get; set; } = default!;
}