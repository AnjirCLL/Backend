using Domain.Entitys.Product;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitys.Users;

public class ClientUser : BaseModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 3)]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(128)]
    public string? LastName { get; set; }

    [StringLength(128)]
    public string? MiddleName { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 12)]
    public string Phone { get; set; } = null!;

    [StringLength(6)]
    public string VerificationCode { get; set; } = string.Empty;

    public DateTime VerificationCodeValidTill { get; set; }

    public DateTime? LastLogin { get; set; }
    public ICollection<ProductViews> ProductViews { get; set; } = default!;
}
