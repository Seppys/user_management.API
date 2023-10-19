namespace User_management.API.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public UserInfo(User user) 
        {
            Id = user.UserId;
            Username = user.Username;
            Email = user.Email;
            Role = user.Role.ToString();
        }
    }
}
