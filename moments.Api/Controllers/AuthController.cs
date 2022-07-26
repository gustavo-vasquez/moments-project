using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using moments.Api.Resources;
using moments.Core;
using moments.Core.Models;

namespace moments.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthController(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            //this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        [HttpGet("[action]")]
        public IActionResult Saludo()
        {
            return Ok("hooooola!");
        }

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
        public async Task<IActionResult> SignIn(UserLoginResource userLoginResource)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == userLoginResource.Email);

            if(user is null)
                return NotFound($"No existe ningún usuario con el correo {userLoginResource.Email}.");

            var passwordIsValid = await _userManager.CheckPasswordAsync(user, userLoginResource.Password);

            if(passwordIsValid)
                return Ok();

            return BadRequest("Email o contraseña incorrecta.");
        }

        [HttpPost("Role")]
        public async Task<IActionResult> NewRole(string roleName)
        {
            if(string.IsNullOrWhiteSpace(roleName))
                return BadRequest("No se especificó nombre para el nuevo rol.");
            
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
    }
}