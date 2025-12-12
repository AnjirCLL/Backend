using Domain.Entities.SearchHistorys;
using Domain.Entities.Shops;
using Domain.Entities.Statistic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Products;

public class Product : BaseModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public int ShopId { get; set; }

    [MaxLength(50)]
    public string? Size { get; set; }

    [Column(TypeName = "numeric(10, 2)")] 
    [Required]
    public decimal Price { get; set; }

    public bool IsActive { get; set; } = true;

    [Required]
    [MaxLength(255)]
    public string? Name { get; set; }

    [MaxLength(255)]
    public string? BrandName { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    [MaxLength(50)]
    public string? Color { get; set; }

    [MaxLength(100)]
    public string? Country { get; set; }

    public string? ImageList { get; set; }

    [Column(TypeName = "numeric(10, 2)")]
    public decimal? DiscountPrice { get; set; }

    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [ForeignKey(nameof(ShopId))]
    public virtual Shop? Shop { get; set; }

    public virtual ICollection<LikeStatistic> Likes { get; set; }
    public virtual ICollection<Commentary> Comments { get; set; }
    public virtual ICollection<SearchHistory> SearchHistoryEntries { get; set; }
    public virtual ICollection<RatingStatistic> Ratings { get; set; }
    public virtual ICollection<ViewStatistic> Views { get; set; }
}