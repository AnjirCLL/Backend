using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.CRM;

public class ClientCallMe : BaseModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(1000)]
    public string FullName { get; set; } = null!;

    [Required]
    [StringLength(20, MinimumLength = 12)]
    public string Phone { get; set; } = null!;

    public DeviceTypeEnum DeviceType { get; set; }

    [StringLength(1000)]
    public string? ActionPath { get; set; }

    [Required]
    [StringLength(120)]
    public string CallStatus { get; set; } = null!;

    public DateTime? JoinTime { get; set; }
    public Guid? JoinUserId { get; set; }
    public ControlUser JoinUser { get; set; } = default!;

    [StringLength(1000)]
    public string? CloseComment { get; set; }
    public DateTime? CloseTime { get; set; }
}
