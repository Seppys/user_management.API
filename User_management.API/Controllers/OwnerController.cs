using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User_management.API.Models;
using User_management.API.Services;

namespace User_management.API.Controllers
{
    [Route("api/owner")]
    [ApiController]
    [Authorize(Policy = ("RequireOwnerRole"))]
    public class OwnerController : ControllerBase
    {
        private readonly UsersContext _usersContext;
        private UserService _userService;

        public OwnerController(UsersContext usersContext, UserService userService)
        {
            _usersContext = usersContext;
            _userService = userService;
        }

        [HttpGet("setadmin/{userId}")]
        public IActionResult SetAdmin(int userId)
        {
            var user = _userService.GetUserFromUserId(userId);
            string userRole = user.Role.ToString();

            if (user == null)
                return NotFound("user not found");
            else if (userRole != "User")
                return BadRequest("user has not user role");

            user.Role = Role.Admin;
            _usersContext.SaveChanges();

            return Ok();
        }

        [HttpGet("removeadmin/userId")]
        public IActionResult RemoveAdmin(int userId)
        {
            var user = _userService.GetUserFromUserId(userId);
            string userRole = user.Role.ToString();

            if (user == null)
                return NotFound("user not found");
            else if (userRole != "Admin")
                return BadRequest("user is not admin");

            user.Role = Role.User;
            _usersContext.SaveChanges();

            return Ok();
        }

        [HttpGet("deleteuser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var user = _userService.GetUserFromUserId(userId);
            var userRegister = _userService.GetUserRegisterFromId(userId);

            if (user == null)
                return NotFound("user not found");
            else if (_userService.GetRoleFromId(userId) == "Owner")
                return BadRequest("Owner cannot be deleted");

            _usersContext.UserRegister.Remove(userRegister);
            _usersContext.UserInformation.Remove(user);
            _usersContext.SaveChanges();

            return Ok();
        }
    }
}
