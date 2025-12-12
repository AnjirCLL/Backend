using Domain.Entities.Products;
using Domain.Entities.Statistic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Shops;

public class Shop : BaseModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    public string  ImageFonUrl { get; set; } = null!;
    public string  ImageIconUrl { get; set; } = null!;
    public double? Longitude { get; set; } 
    public double? Latitude { get; set; } 
    public string? TelegramUrl { get; set; }
    public string? InstagramUrl { get; set; } 
    public string? WebsiteUrl { get; set; }

    [MaxLength(20)]
    public string Inn { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [MaxLength(20)]
    public string ShopPhoneNumber { get; set; } = null!;
    public int ProductCount { get; set; } = 0;
    public string WorkTime { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = null!;
    public ICollection<SubscribeStatistic> Subscribers { get; set; } = null!;
    public ICollection<RatingStatistic> ShopRatings { get; set; } = null!;
    public ICollection<PhoneStatistic> PhoneClickStatistics { get; set; } = null!;
    public ICollection<ProfileStatistic> ProfileViewStatistics { get; set; } = null!;
    public ICollection<ViewStatistic> ViewStatistics { get; set; } = null!;
}
