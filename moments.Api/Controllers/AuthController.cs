using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using moments.Api.Resources;
using moments.Core;
using moments.Core.Models;
using moments.Core.Services;
using moments.Services;

namespace moments.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<User> userManager, RoleManager<Role> roleManager, IOptionsSnapshot<JwtSettings> _jwtSettings, ITokenService tokenService)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._jwtSettings = _jwtSettings.Value;
            this._tokenService = tokenService;
        }

#region SALUDOS DE PRUEBA

        [HttpGet("[action]")]
        [Authorize("elbromas")]
        public IActionResult SaludoJoker()
        {
            return Ok("hooooola!");
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Administrador")] // politica personalizada en la que tiene que cumplir el nombre de usuario indicado
        public IActionResult SaludoAdministrador()
        {
            return Ok("Hola administrador");
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Usuario")]
        public IActionResult SaludoUsuario()
        {
            return Ok("Hola usuario");
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Temporal")]
        public IActionResult SaludoTemporal()
        {
            return Ok("Hola temporal");
        }

#endregion

# region LOGIN Y REGISTRO

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(UserSignUpResource userSignUpResource)
        {
            User newUser = new User()
            {
                Email = userSignUpResource.Email,
                UserName = userSignUpResource.UserName,
                NickName = userSignUpResource.NickName,
                Birthdate = DateTime.Now
            };

            IdentityResult newUserResult = await _userManager.CreateAsync(newUser, userSignUpResource.Password);

            if(newUserResult.Succeeded)
                return Ok();

            return Problem(newUserResult.Errors.First().Description, null, 500);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(UserLoginResource userLoginResource)
        {
            User user = _userManager.Users.SingleOrDefault(u => u.Email == userLoginResource.Email);

            if(user is null)
                return NotFound($"No existe ningún usuario con el correo {userLoginResource.Email}.");

            bool passwordIsValid = await _userManager.CheckPasswordAsync(user, userLoginResource.Password);

            if(passwordIsValid)
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(user); // los roles a los que el usuario activo pertenece
                //return Ok(GenerateAccessToken(user, userRoles)); // crear el token para el usuario logueado
                var accessToken = _tokenService.GenerateAccessToken(user, userRoles, _jwtSettings);
                var refreshToken = await _tokenService.GenerateRefreshToken(user.Id);

                return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
            }

            return BadRequest("Email o contraseña incorrecta.");
        }

        [HttpPost("{username}/[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ReNewToken(string username, [FromForm] string refreshTokenValue)
        {
            User user = await _userManager.FindByNameAsync(username);

            if(user is null)
                return NotFound($"Usuario '{username}' no encontrado.");

            IList<string> userRoles = await _userManager.GetRolesAsync(user); // los roles a los que el usuario activo pertenece

            try
            {
                return Ok(await _tokenService.ReNewTokens(refreshTokenValue, userRoles, _jwtSettings));
            }
            catch(Exception ex)
            {
                if(ex is ArgumentException)
                    return BadRequest(ex.Message);
                
                return Forbid();
            }
        }

#endregion

#region ROLES

        [HttpPost("Role")]
        public async Task<IActionResult> NewRole(string roleName)
        {
            if(string.IsNullOrWhiteSpace(roleName))
                return BadRequest("No se especificó nombre para el nuevo rol.");

            if(await _roleManager.RoleExistsAsync(roleName))
                return Problem($"El rol '{roleName}' ya existe.", null, 500);
            
            var newRole = new Role
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if(roleResult.Succeeded)
                return Ok();

            return Problem(roleResult.Errors.First().Description, null, 500);
        }
        
        [HttpGet("Role")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoleMembers(string roleName)
        {
            if(await _roleManager.RoleExistsAsync(roleName))
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

                if(usersInRole.Count() > 0)
                    return Ok(usersInRole);
                else
                    return NoContent();
            }
            else
                return NotFound($"El rol '{roleName}' no existe.");
        }

        [HttpPost("User/{email}/Role")]
        public async Task<IActionResult> AddUserToRole(string email,[FromForm] string roleName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == email);

            if(user is null)
                return NotFound($"No se puede asignar rol. Causa: No existe ningún usuario con el correo {email}.");

            if(await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);

                if(result.Succeeded)
                    return Ok();

                return Problem(result.Errors.First().Description, null, 500);
            }
            else
                return NotFound($"No se puede asignar rol. Causa: El rol '{roleName}' no existe.");
        }

        [HttpDelete("User/{email}/role")]
        public async Task<IActionResult> RemoveUserFromRole(string email, [FromForm] string roleName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == email);

            if(user is null)
                return NotFound($"No se puede eliminar el rol. Causa: No existe ningún usuario con el correo {email}.");

            if(await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);

                if(result.Succeeded)
                    return Ok();

                return Problem(result.Errors.First().Description, null, 500);
            }
            else
                return NotFound($"No se puede eliminar el rol. Causa: El rol '{roleName}' no existe.");
        }

#endregion

        /* private string GenerateAccessToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                // datos que va a contener el token
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // id de usuario (como subject del jwt)
                new Claim(ClaimTypes.Name, user.UserName), // nombre de usuario
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // id del token
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // id de usuario
            };

            IEnumerable<Claim> roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)); // creo un claim para cada rol al que pertenezca el usuario y lo agrego a la lista
            claims.AddRange(roleClaims); // agrego todos los roles al token

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(Convert.ToDouble(_jwtSettings.ExpirationInHours)); // tiempo de expiración (valor extraído de appsettings)

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        } */
    }
}