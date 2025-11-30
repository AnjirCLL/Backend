using Domain.Enums;

namespace Domain.Entitys.CRM;

public class SuperAdmin : BaseModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; } 
    public string? Username { get; set; } 
    public string? PasswordHash { get; set; }   
    public Role Role { get; set; } 
}
