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

namespace moments.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly JwtResource _jwtResource;

        public AuthController(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<Role> roleManager, IOptionsSnapshot<JwtResource> jwtResource)
        {
            //this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._jwtResource = jwtResource.Value;
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult Saludo()
        {
            return Ok("hooooola!");
        }

# region LOGIN Y REGISTRO

        [HttpPost("[action]")]
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
                return Ok(GenerateJwt(user, userRoles)); // crear el token para el usuario logueado
            }

            return BadRequest("Email o contraseña incorrecta.");
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

        [AllowAnonymous]
        [HttpGet("Role")]
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

        private string GenerateJwt(User user, IList<string> roles)
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

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtResource.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtResource.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtResource.Issuer,
                audience: _jwtResource.Issuer,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}