using System.ComponentModel.DataAnnotations;

namespace fullstack_project_1.Data.Authentication
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }

    }

}
