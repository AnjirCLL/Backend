using Domain.Entities.Shops;
using Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Statistic;

public class ProfileStatistic : BaseModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ShopId { get; set; }

    [Required]
    [MaxLength(50)]
    public string IPAddress { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(ShopId))]
    public Shop Shop { get; set; } = null!;
}
