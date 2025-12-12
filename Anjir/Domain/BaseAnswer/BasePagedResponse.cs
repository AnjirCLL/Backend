namespace Domain.BaseAnswer;
public class BasePagedResponse<T>
{
    public int? Offset { get; set; }
    public int? Limit { get; set; }
    public List<T> Items { get; set; } = null!;

    public int? Total { get; set; }
    public string? SearchText { get; set; }
    public string? OrderBy { get; set; }
    public bool OrderDesc { get; set; }
}

