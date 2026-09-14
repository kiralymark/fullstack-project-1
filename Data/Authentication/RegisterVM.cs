using System.ComponentModel.DataAnnotations;

namespace fullstack_project_1.Data.Authentication
{
    public class RegisterVM
    {
        // a custom view model


        [Required(ErrorMessage = "Username is required")]   // must provide this data, otherwise throw an error
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Role is required")]
        //public string Role { get; set; }

    }
}
