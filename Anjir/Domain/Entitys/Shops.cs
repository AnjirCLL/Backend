using Domain.Entitys.CRM;
using Domain.Entitys.Pay;
using Domain.Entitys.Product;

namespace Domain.Entitys;

public class Shops : BaseModel
{
    public Guid Id { get; set; }
    public string? ShopName { get; set; }
    public string? OwnerName { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
    public DateTime SubscriptionExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }


    public ICollection<AdminShop> Admins { get; set; } = default!;
    public ICollection<Products> Products { get; set; } = default!;
    public ICollection<Payments> Payments { get; set;  } =default!;
}
