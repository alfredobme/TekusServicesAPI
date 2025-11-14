using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TekusServices.Application.DTO;
using TekusServices.Application.Interfaces;

namespace TekusServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IJwtUtils jwtUtils, IUserService userService) : ControllerBase
    {
        private readonly IJwtUtils _jwtUtils = jwtUtils;
        private readonly IUserService _userService = userService;

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            try
            {
                LoginDTO? login = AutorizarTransaccion(this.Request.Headers.Authorization.ToString());
                if (login == null)
                {
                    return Unauthorized(new { Message = "Authorization data is not valid. Contact the administrator" });
                }

                UserDTO? user = await _userService.GetUser(login.User);
                if (user == null)
                {
                    return Unauthorized(new { Message = "Incorrect username or password." });
                }

                if (user.Code != login.Password)
                {
                    return Unauthorized(new { Message = "Incorrect username or password." });
                }
                var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Code),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = _jwtUtils.GenerateToken(authClaims);
                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    expiration = token.ValidTo,
                    user.Code
                });
            }
            catch
            {
                return StatusCode(500, new { Message = "Error Generando Autenticación" });
            }
        }

        private static LoginDTO? AutorizarTransaccion(string autorizacion)
        {
            LoginDTO? user = null;
            if (autorizacion != null && autorizacion.StartsWith("Basic"))
            {
                string encodedUsernamePassword = autorizacion["Basic ".Length..].Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                var splitUserPass = usernamePassword.Split(':');

                if (splitUserPass.Length >= 2 && !string.IsNullOrWhiteSpace(splitUserPass[0]) && !string.IsNullOrWhiteSpace(splitUserPass[1]))
                {
                    user = new LoginDTO { User = splitUserPass[0], Password = splitUserPass[1] };
                    return user;
                }
                return user;
            }
            else
            {
                return user;
            }
        }
    }
}
