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
    public int UserId { get; set; }

    public int? ProductId { get; set; }

    [Required]
    [MaxLength(255)]
    public string? SearchTxt { get; set; }

    [MaxLength(50)]
    public string SearchType { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product? Product { get; set; }
}
