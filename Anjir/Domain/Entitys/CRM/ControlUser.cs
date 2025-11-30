using System.ComponentModel.DataAnnotations;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;

namespace Domain.Entitys.CRM;

public class ControlUser : BaseModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 3)]
    public string FullName { get; set; } = null!;

    [IndexColumn(IsUnique = true)]
    [StringLength(64, MinimumLength = 4)]
    public string Login { get; set; } = null!;

    public byte[] StoredPassword { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;

    public bool IsSenior { get; set; }

    public DateTime? LastLogin { get; set; }


}
