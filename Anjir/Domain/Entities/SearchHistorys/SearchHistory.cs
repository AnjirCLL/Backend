using Domain.Entities.Products;
using Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.SearchHistorys;

public class SearchHistory : BaseModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    public Guid? ProductId { get; set; }

    [Required]
    [MaxLength(255)]
    public string SearchTxt { get; set; } = null!;

    [MaxLength(50)]
    public string SearchType { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }
}
