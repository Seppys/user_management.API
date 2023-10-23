using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User_management.API.Models;
using User_management.API.Services;

namespace User_management.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UsersContext _usersContext;
        private readonly UserService _userService;

        public UserController (UsersContext usersContext, UserService userService)
        {
            _usersContext = usersContext;
            _userService = userService;
        }

        [HttpGet("getme")]
        public IActionResult GetCurrentUser()
        {
            User user = _userService.GetUserFromClaims(HttpContext.User);

            if (user == null)
                return NotFound("user doesn't exist");

            UserInfo userInfo = new UserInfo(user);
            return Ok(userInfo);
        }

        [HttpPost("changeusername")]
        public IActionResult ChangeUsername (InfoChangeModels.ChangeUsernameModel model)
        {
            User user = _userService.GetUserFromClaims(HttpContext.User);

            user.Username = model.NewUsername;
            _usersContext.SaveChanges();

            return Ok("username changed, please login again");
        }

        [HttpPost("changeemail")]
        public IActionResult ChangeEmail (InfoChangeModels.ChangeEmailModel model)
        {
            User user = _userService.GetUserFromClaims(HttpContext.User);

            user.Email = model.NewEmail;
            _usersContext.SaveChanges();

            return Ok("email changed");
        }

        [HttpPost("changepassword")]
        public IActionResult ChangePassword (InfoChangeModels.ChangePasswordModel model)
        {
            User user = _userService.GetUserFromClaims(HttpContext.User);

            if (user.Password == PasswordHasherService.HashPassword(model.CurrentPassword))
                return BadRequest("current password is incorrect");

            user.Password = PasswordHasherService.HashPassword(model.NewPassword);
            return Ok("password changed");
        }
    }
}
