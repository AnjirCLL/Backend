using Domain.Entities.Products;
using Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Statistic;

public class Commentary : BaseModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public string? Word { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product? Product { get; set; }
}
