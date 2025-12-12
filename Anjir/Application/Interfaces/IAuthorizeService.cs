using Domain.Entities.Authorize;
using Domain.Enums;

namespace Application.Interfaces;

public interface IAuthorizeService
{
    string Authorize(AuthorizeUser user, DateTime expires);
    Guid GetId();
    string GetName();
    UserTypeEnum GetUserType();
    bool GetIsSenior();
}
