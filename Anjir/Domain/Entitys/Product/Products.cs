namespace Domain.Entitys.Product;

public class Products : BaseModel
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }

    public Shops Shop { get; set; } = default!;
    public ICollection<ProductViews> ProductViews { get; set; } =default!;
}
