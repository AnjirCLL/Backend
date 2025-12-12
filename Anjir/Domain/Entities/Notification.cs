using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities;

public class Notification : BaseModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public DeviceType Device { get; set; } 
    public string? Description { get; set; }    
    public int ViewCount { get; set; }
    public DateTimeOffset? ValidDate { get; set; }
}
