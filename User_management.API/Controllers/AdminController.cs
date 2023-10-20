using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User_management.API.Models;
using User_management.API.Services;

namespace User_management.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : ControllerBase
    {
        private readonly UsersContext _usersContext;
        private readonly UserService _userService;

        public AdminController(UsersContext usersContext, UserService userService)
        {
            _usersContext = usersContext;
            _userService = userService;
        }

        [HttpGet("getallusers")]
        public IActionResult GetAllUsers()
        {
            List<UserInfo> allUsers = _userService.GetAllUsers();
            return Ok(allUsers);
        }

        [HttpGet("getuser/{id}")]
        public IActionResult GetUserById(int id)
        {
            User user = _userService.GetUserFromUserId(id);
            UserInfo userInfo = new UserInfo(user);
            return Ok(userInfo);
        }

        [HttpGet("deleteuser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var user = _userService.GetUserFromUserId(userId);

            if (user == null)
                return NotFound("user not found");
            else if (_userService.GetRoleFromId(userId) == "Owner")
                return BadRequest("Owner cannot be deleted");

            _usersContext.User.Remove(user);
            _usersContext.SaveChanges();

            return Ok();
        }
    }
}
