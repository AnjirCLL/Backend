using Domain.Entitys.Users;

namespace Domain.Entitys.Product;   

public class ProductViews : BaseModel
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public DateTime ViewedAt { get; set; } = default!;


    public Products Product { get; set; }  = default!;
    public ClientUser User { get; set; } = default!;
}
