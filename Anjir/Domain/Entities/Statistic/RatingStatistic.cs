using Domain.Entities.Products;
using Domain.Entities.Shops;
using Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Statistic;

public class RatingStatistic : BaseModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]  
    public int UserId { get; set; }

    public int? ProductId { get; set; }

    [Required]
    public int ShopId { get; set; }

    [Required]
    public int RatingDegree { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }

    [Required]
    [MaxLength(50)]
    public string IPAddress { get; set; } = null!;

    [ForeignKey(nameof(ProductId))]
    public virtual Product? Product { get; set; }

    [ForeignKey(nameof(ShopId))]
    public virtual Shop? Shop { get; set; }
}
