namespace Domain.Entitys.Pay;

public class Payments : BaseModel
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string? Status { get; set; }


    public Shops Shop { get; set; } = default!;
}
