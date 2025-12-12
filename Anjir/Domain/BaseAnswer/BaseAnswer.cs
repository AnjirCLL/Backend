namespace Domain.BaseAnswer;

public class BaseAnswer<T>
{
    public bool Succeeded { get; set; }
    public List<string>? Messages { get; set; }
    public T? Data { get; set; }
}
