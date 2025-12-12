using Domain.Entities.SearchHistorys;
using Domain.Entities.Statistic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Users;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Fullname { get; set; }

    public int? Age { get; set; }

    [MaxLength(50)]
    public string? Gender { get; set; } 

    public string? ImageUrl { get; set; }

    [Required]
    [MaxLength(20)]
    public string? Phone { get; set; } // Unique

    public double? Longitude { get; set; }
    public double? Latitude { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public virtual ICollection<SubscribeStatistic> Subscriptions { get; set; }
    public virtual ICollection<LikeStatistic> Likes { get; set; }
    public virtual ICollection<Commentary> Comments { get; set; }
    public virtual ICollection<SearchHistory> SearchHistory { get; set; }
    public virtual ICollection<RatingStatistic> Ratings { get; set; }
    public virtual ICollection<PhoneStatistic> PhoneClicks { get; set; }
    public virtual ICollection<ProfileStatistic> ProfileViews { get; set; }
    public virtual ICollection<ViewStatistic> Views { get; set; }
}
