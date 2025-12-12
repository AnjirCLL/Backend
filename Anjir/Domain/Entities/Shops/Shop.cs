using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Shops;

public  class Shop : BaseModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Name { get; set; }

    public string? ImageFonUrl { get; set; }
    public string? ImageIconUrl { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public string? TelegramUrl { get; set; }
    public string? InstagramUrl { get; set; }

    [MaxLength(20)]
    public string? Inn { get; set; } 

    [Required]
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [MaxLength(20)]
    public string? ShopPhoneNumber { get; set; }

    public string? WebsiteUrl { get; set; }
    public int ProductCount { get; set; } = 0;
    public string? WorkTime { get; set; }

    public virtual ICollection<Product> Products { get; set; }
    public virtual ICollection<SubscribeStatistic> Subscribers { get; set; }
    public virtual ICollection<RatingStatistic> ShopRatings { get; set; }
    public virtual ICollection<PhoneStatistic> PhoneClickStatistics { get; set; }
    public virtual ICollection<ProfileStatistic> ProfileViewStatistics { get; set; }
    public virtual ICollection<ViewStatistic> ViewStatistics { get; set; }
}
