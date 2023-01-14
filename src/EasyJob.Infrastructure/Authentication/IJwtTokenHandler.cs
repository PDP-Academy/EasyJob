using EasyJob.Domain.Entities.Users;
using System.IdentityModel.Tokens.Jwt;

namespace EasyJob.Infrastructure.Authentication;

public interface IJwtTokenHandler
{
    JwtSecurityToken GenerateJwtToken(User user);
}