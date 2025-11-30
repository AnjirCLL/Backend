using System.ComponentModel;

namespace Domain.BaseAnswer;

public class BasePagedRequest
{
    [DefaultValue(20)]
    public int PageSize { get; set; } = 20;

    [DefaultValue(1)]
    public int Page { get; set; } = 1;

    public string? SearchText { get; set; }

    public bool? IsDeleted { get; set; }

    public string? OrderBy { get; set; }
    public bool OrderDesc { get; set; }
}
