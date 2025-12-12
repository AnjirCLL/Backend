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
    public Guid ShopId { get; set; }

    [MaxLength(50)]
    public string Size { get; set; } = null!;

    [Column(TypeName = "numeric(10, 2)")]
    [Required]
    public decimal Price { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    [MaxLength(255)]
    public string BrandName { get; set; } = null!;

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    [MaxLength(50)]
    public string Color { get; set; } = null!;

    [MaxLength(100)]
    public string Country { get; set; } = null!;

    public string ImageList { get; set; } = null!;

    [Column(TypeName = "numeric(10, 2)")]
    public decimal? DiscountPrice { get; set; }

    public string Description { get; set; } = null!;

    [ForeignKey(nameof(ShopId))]
    public Shop Shop { get; set; } = null!;

    public ICollection<LikeStatistic> Likes { get; set; } = null!;
    public ICollection<Commentary> Comments { get; set; } = null!;
    public ICollection<SearchHistory> SearchHistoryEntries { get; set; } = null!;
    public ICollection<RatingStatistic> Ratings { get; set; } = null!;
    public ICollection<ViewStatistic> Views { get; set; } = null!;
}