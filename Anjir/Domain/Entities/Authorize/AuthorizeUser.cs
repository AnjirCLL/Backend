using Domain.Enums;

namespace Domain.Entities.Authorize;

public class AuthorizeUser
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public UserTypeEnum UserType { get; set; }
    public bool IsSenior { get; set; }
}