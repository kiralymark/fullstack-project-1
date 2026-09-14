using fullstack_project_1.Data;
using fullstack_project_1.Data.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace fullstack_project_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        // inject services into api endpoints

        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;                     // connection with the postgresql database 
        private readonly IConfiguration _configuration;             // to access the secrets json

        public AuthenticationController(UserManager<AspNetUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
        }

        // create API endpoints
        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody] RegisterVM payload)    // register vm -- a custom view model with data 'payload'
        {
            /*
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all required fields");
            }
            */

            var userExists = await _userManager.FindByEmailAsync(payload.Email);   

            if (userExists != null)     // check if user's email exists in the database; force unique email addresses
            {
                return BadRequest($"User '{payload.Email}' already exists");
            }

            AspNetUser newUser = new AspNetUser()   // if user's email not exists, create user
            {
                Email = payload.Email,
                UserName = payload.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(newUser, payload.Password); // create user with the registration data

            if (!result.Succeeded)  // check if user creation was not successful, throw an error
            {
                return BadRequest("User could not be created!");
            }

            /*
            switch (payload.Role)
            {
                case "Admin":
                    await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
                    break;
                case "Publisher":
                    await _userManager.AddToRoleAsync(newUser, UserRoles.Publisher);
                    break;
                case "Author":
                    await _userManager.AddToRoleAsync(newUser, UserRoles.Author);
                    break;
                default:
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                    break;
            }
            */

            return Created(nameof(Register), $"User '{payload.Email}' created");
        }




    }
}
