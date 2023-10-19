using Microsoft.AspNetCore.Mvc;
using User_management.API.Models;
using User_management.API.Services;

namespace User_management.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UsersContext _usersContext;
        private readonly UserService _userService;

        public AccountController(UsersContext usersContext, UserService userService)
        {
            _usersContext = usersContext;
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult CreateUser(UserCreation newUserCreation)
        {
            if (_userService.UsernameExists(newUserCreation.Username))
                return BadRequest(new { error = "username already exists" });
            if (_userService.EmailExists(newUserCreation.Email))
                return BadRequest(new { error = "email already exists" });

            _usersContext.User.Add(new Models.User(newUserCreation));
            _usersContext.SaveChanges();

            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest login)
        {
            string passwordHashed = PasswordHasherService.HashPassword(login.Password);
            var user = _usersContext.User.FirstOrDefault(u => u.Username == login.Username &&
                u.Password == passwordHashed);
            if (user != null)
            {
                var userId = user.UserId;
                var username = _userService.GetUsernameFromId(userId);
                var role = _userService.GetRoleFromId(userId);

                var token = TokenService.GenerateTokenJwt(username, role);
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}

