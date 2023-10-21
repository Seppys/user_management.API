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

        [HttpPost("recoverpassword")]
        public IActionResult RecoverPassword(PasswordRecoveryModel model)
        {
            User user = _usersContext.User.FirstOrDefault(u => u.Username == model.Username && u.Email == model.Email);

            if (user == null)
                return BadRequest("incorrect data");

            string newPassword = _userService.GenerateRandomString(10);

            string userEmail = user.Email;
            string subject = "Password recovery";
            string body = $"Your new password is <{newPassword}>. Please change it as soon as possible";

            user.Password = PasswordHasherService.HashPassword(newPassword);
            _usersContext.SaveChanges();

            EmailService.SendEmail(userEmail, subject, body);

            return Ok("An email with your new password has been sent to your email");
        }
    }
}

