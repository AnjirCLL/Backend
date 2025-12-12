using Application.Interfaces;
using Domain.Entities.Authorize;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Sevices.Authorize;

public class AuthorizeService(IConfiguration configuration, IHttpContextAccessor accessor) : IAuthorizeService
{
    private readonly string jwtSecret = configuration["JWT:Secret"] ?? "";
    private readonly IHttpContextAccessor accessor = accessor;

    public string Authorize(AuthorizeUser user, DateTime expires)
    {
        var handler = new JwtSecurityTokenHandler();
        var privateKey = Encoding.UTF8.GetBytes(jwtSecret);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = expires,
            Subject = GenerateClaims(user)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(AuthorizeUser user)
    {
        ClaimsIdentity ci = new();
        ci.AddClaim(new Claim("id", user.Id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
        ci.AddClaim(new Claim("userType", user.UserType.ToString()));
        ci.AddClaim(new Claim("isSenior", user.IsSenior.ToString()));
        return ci;
    }

    public Guid GetId() => Guid.TryParse(accessor.HttpContext?.User?.FindFirst("id")?.Value ?? "", out Guid id) ? id : Guid.Empty;
    public bool GetIsSenior() => accessor.HttpContext?.User?.FindFirst("isSenior")?.Value == "True";
    public string GetName() => accessor.HttpContext?.User?.FindFirst(ClaimTypes.GivenName)?.Value ?? "";

    public UserTypeEnum GetUserType()
    {
        var _userType = accessor.HttpContext?.User?.FindFirst("userType")?.Value;
        bool isEnum = Enum.TryParse(_userType, out UserTypeEnum userType);
        return isEnum ? userType : UserTypeEnum.Client;
    }
}
