using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TekusServices.Application.Interfaces
{
    public interface IJwtUtils
    {
        public JwtSecurityToken GenerateToken(List<Claim> authClaims);
    }
}
