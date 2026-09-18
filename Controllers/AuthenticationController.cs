using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using fullstack_project_1.Data;
using fullstack_project_1.Data.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        private string secretString;
        private string issuerString;
        private string audienceString;

        public AuthenticationController(UserManager<AspNetUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;

            // Retrieve configuration values before setting options
            secretString = _configuration["JWT:Secret"]
                ?? throw new InvalidOperationException("Configuration string 'JWT:Secret' not found.");

            issuerString = _configuration["JWT:Issuer"]
                ?? throw new InvalidOperationException("Configuration string 'JWT:Issuer' not found.");

            audienceString = _configuration["JWT:Audience"]
                ?? throw new InvalidOperationException("Configuration string 'JWT:Audience' not found.");
        }
        
        // create API endpoints
        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody] RegisterVM payload)    // register vm -- a custom view model with data 'payload'
        {
            if (!ModelState.IsValid)        // to check if both the user (email) field and password is provided
            {
                return BadRequest("Please, provide all required fields");
            }

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

        [HttpPost("login-user")]
        public async Task<IActionResult> Login([FromBody]LoginVM payload)       // User Login handling
        {
            if (!ModelState.IsValid)        // to check if both the user (email) field and password is provided
            {
                return BadRequest("Please, provide all required fields");
            }
            
            var user = await _userManager.FindByEmailAsync(payload.Email);

            if(user != null && await _userManager.CheckPasswordAsync(user, payload.Password))   // to check user password and give back a JWT token (if successful)
            {
                var tokenValue = await GenerateJwtToken(user);

                return Ok(tokenValue);
            }

            return Unauthorized();
        }

        private async Task<AuthResultVM> GenerateJwtToken(AspNetUser user)      // generate the JWT token
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),                     // subject
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())       // JWT ID, a unique identifier for that specific token
            };

            //Add User Roles
            /*
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            */

            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretString));

            var token = new JwtSecurityToken(
                issuer: issuerString,
                audience: audienceString,
                expires: DateTime.UtcNow.AddMinutes(2), // expires: DateTime.UtcNow.AddMinutes(10), ; (usually it is set to 5 - 10 mins)
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            
            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(6),  // refresh token expiration date set to 6 months
                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
            };
            await _context.RefreshTokens.AddAsync(refreshToken);    
            await _context.SaveChangesAsync();                      // add the refresh token to the database

            var response = new AuthResultVM()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = token.ValidTo               // assign the same value from the parameter (var token -- > 'expires')
            };
            
            return response;
        }

    }
}
