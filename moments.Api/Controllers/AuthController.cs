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

        public AuthController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            //this._unitOfWork = unitOfWork;
            this._userManager = userManager;
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
    }
}