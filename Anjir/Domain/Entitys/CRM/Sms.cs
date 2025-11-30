using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitys.CRM;

public class Sms : BaseModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 10)]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [StringLength(700, MinimumLength = 3)]
    public string Message { get; set; } = null!;

    public SmsTypeEnum Type { get; set; }

    [StringLength(120)]
    public string? Data { get; set; }
}
