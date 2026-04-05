using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.Auth
{
    public class RegisterModel
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;
    }
}
