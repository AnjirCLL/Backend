using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Carousel : BaseModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(1000)]
    public string Name { get; set; } = null!;

    public int OrderNumber { get; set; }

    [StringLength(250)]
    public string? Type { get; set; }

    public CarouselDestinationEnum Destination { get; set; }

    public string? Language { get; set; }

    public DeviceTypeEnum DeviceType { get; set; }

    [Required]
    [StringLength(500)]
    public string ImageUrl { get; set; } = null!;

    [StringLength(1000)]
    public string? ClickUrl { get; set; }

    public DateTime? ValidDateFrom { get; set; }

    public DateTime? ValidDateTo { get; set; }
}
