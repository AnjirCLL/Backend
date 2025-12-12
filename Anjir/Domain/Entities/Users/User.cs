using Domain.Entities.SearchHistorys;
using Domain.Entities.Statistic;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Users;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Fullname { get; set; } = null!;
    public string BirthDay { get; set; } = null!;

    [MaxLength(50)]
    public Gender Gender { get; set; }
    public string ImageUrl { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; } = null!;

    public double? Longitude { get; set; }
    public double? Latitude { get; set; }

    public ICollection<SubscribeStatistic> Subscriptions { get; set; } = null!;
    public ICollection<LikeStatistic> Likes { get; set; } = null!;
    public ICollection<Commentary> Comments { get; set; } = null!;
    public ICollection<SearchHistory> SearchHistory { get; set; } = null!;
    public ICollection<RatingStatistic> Ratings { get; set; } = null!;
    public ICollection<PhoneStatistic> PhoneClicks { get; set; } = null!;
    public ICollection<ProfileStatistic> ProfileViews { get; set; } = null!;
    public ICollection<ViewStatistic> Views { get; set; } = null!;
}
