using Domain.Enums;

namespace Domain.Entitys.CRM;

public class AdminShop
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public string? FullName { get; set; }
    public string? Phone { get; set; }
    public string? PasswordHash { get; set; }
    public Role Role { get; set; } = Role.ShopAdmin;
}
