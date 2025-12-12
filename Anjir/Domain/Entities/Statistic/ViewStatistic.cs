using Domain.Entities.Products;
using Domain.Entities.Shops;
using Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Statistic;

public class ViewStatistic : BaseModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public Guid ShopId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string IPAddress { get; set; } = null!;

    [ForeignKey(nameof(ShopId))]
    public Shop Shop { get; set; } = null!;
}
