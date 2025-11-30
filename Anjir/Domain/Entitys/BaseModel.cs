namespace Domain.Entitys;

public class BaseModel
{
    public DateTime CreateTime { get; set; }
    public DateTime? UpdateTime { get; set; }
    public Guid CreateUserId { get; set; }
    public Guid? UpdateUserId { get; set; }

    public bool IsDeleted { get; set; }
}
